# Claude Code Configuration for Unity C# Project

## 1. Development Guidelines

- **Environment:** Windows 11, PowerShell, VS Code, C# Dev Kit
- **Unity Version:** 2022.3 LTS line (project currently 2022.3.21f1)
- **Editor:** VS Code 1.102.x with Unity and C# extensions
- **Language:** C# 10 or Unity project default — follow Unity’s supported features
- **Style:** Prefer explicit, readable code over cleverness or micro-optimizations
- **Architecture:** Object-Oriented Programming wherever appropriate
- **Structure & Naming:**

  - `PascalCase` for classes and public members
  - `camelCase` for private fields
  - `_camelCase` or `m_` prefix for serialized private fields — pick one and stay consistent

- Avoid ternary operators — use explicit `if/else` for clarity
- Prefer early returns to flatten control flow
- Keep classes small, focused, and with a single responsibility
- Separate Unity lifecycle code from pure logic:

  - Runtime logic in `MonoBehaviour` only if Unity lifecycle is required
  - Pure logic in plain C# classes/structs inside assembly definitions

- Use `ScriptableObject` for configuration and light state — treat as read-only where possible
- Use `[SerializeField] private` for inspector-wired fields; avoid `public` fields
- Avoid `FindObjectOfType`, `Find`, or string-based lookups — wire references via inspector or a small composition root
- Prefer composition over singletons — if a singleton is used, make it explicit and well documented
- Coroutines are fine for frame-based flow; use `async` only for pure C# tasks or non-Unity I/O
- Never call Unity API from background threads
- Replace magic numbers with named `const` or `static readonly` fields
- Log intentionally — use `Debug.LogError` for actionable failures, `Debug.LogWarning` for survivable anomalies

> Analogy: treat `MonoBehaviour` scripts like actors on stage — they take cues and references. Keep heavy thinking in backstage classes.

---

## 2. Clean Code Principles

- **Descriptive Names:** Example — `CalculateLegalMoves`, `BoardState`, `MoveValidator`
- **Small Methods:** One verb per method, avoid deep nesting
- **No Duplication:** Extract helpers for repeated logic
- **No Hidden Magic:** Make data flow obvious through parameters and return values
- **Architectural Consistency:**

  - If code usage and class definitions disagree, this is a red flag
  - Investigate the actual class structure before changing calls
  - Fix architecture, not the call site hack
  - If a class needs new members, add them via constructor or proper methods — do not bypass initialization logic

- **Minimal Global State:** Pass dependencies explicitly, avoid hard singletons
- **Comment for Intent Only:** Update or remove stale comments
- **Consistency Over Cleverness:** Maintain a clean, predictable architecture even if it’s less “smart”
- **Readable First:** Prioritize maintainability over performance unless proven necessary

---

## 3. Testing Guidelines

**Framework:** Unity Test Framework with NUnit

**Test Types:**

- **EditMode:** For pure logic and fast feedback
- **PlayMode:** For integration with scenes, prefabs, and Unity lifecycles

**Expectations:**

- Every new function or logic change must have tests — no exceptions without explanation
- If a test is not practical, explain why and what would make it testable
- Use real types and constructors; avoid faking internals unless testing that behavior
- Never override object internals unless explicitly testing that override
- Let constructors run full initialization and use results in tests
- Keep test names behavior-focused: `ItRejectsMovesThatLeaveKingInCheck`
- DRY test setup with builders/helpers
- Prefer deterministic data over mocks; mock only external boundaries
- Never assume a property exists without verifying in constructor or dynamically
- If mismatch found between class definition and usage, fix usage — not the test

**Folder Layout Example:**

```
Assets/Tests/EditMode/Board/
Assets/Tests/PlayMode/Integration/
```

**TDD Loop:**

1. **Red:** Write a failing EditMode test for a single rule
2. **Green:** Implement minimal code to pass
3. **Refactor:** Clean up names and helpers without changing behavior
4. Repeat for PlayMode tests when scene wiring is involved

---

## 4. Comment Philosophy

- Only comment when **intent** or **assumptions** are not obvious from code
- Never narrate what obvious code does — make the code explain itself
- Use XML docs for public APIs that form contracts
- Use `// why` comments for engine quirks or non-obvious decisions
- Keep comments truthful and updated — delete outdated ones
- Tags:

  - `// TODO:` improvements that don’t block correctness
  - `// FIXME:` correctness issues that must be fixed
  - `// HACK:` temporary engine or package workarounds

**Good Example:**

```csharp
// Skip first legal move — it's a placeholder inserted by the generator
for (int i = 1; i < legalMoves.Count; i++) { ... }
```

---

## 5. Git Commit Guidelines

**Format:**

```
<type>: <short summary>

<detailed explanation, if needed>
```

**Types:** `feat`, `fix`, `refactor`, `test`, `docs`, `chore`

**Content Rules:**

- One logical change per commit
- Bundle logic and tests when appropriate
- Each commit must compile and pass local tests
- Use bullets for multiple points
- Explain reasoning for reversals or risky changes

**Review Process:**

- Never commit automatically
- After each change, output:

  - Staged filenames
  - Final commit message
  - Short “what & why” summary

- Wait for confirmation before proceeding

**Example:**

```
Staged files:
- Assets/Scripts/Core/Board/MoveValidator.cs
- Assets/Tests/EditMode/Board/MoveValidatorTests.cs

Commit message:
feat: add check detection and integrate with move validation

- Introduce ICheckRule and CheckDetection
- Extend MoveValidator to reject illegal king moves
- Add EditMode tests for check scenarios
```

---

## 6. Task Scope & Refactoring

- Large refactors must be split into small, testable steps
- If full change doesn’t fit in one task, clearly state what’s done and what remains
- Mark questionable old code with `// legacy:` and explain risk
- Don’t delete unknown code without confirmation
- Prefer targeted refactors with tests to lock behavior
- If architecture is unclear, propose a small diagram for approval

---

## 7. Unity Specific Practices

- `Awake` for internal setup
- `Start` for cross-object wiring after all `Awake` calls
- `OnEnable`/`OnDisable` for event subscriptions
- Avoid allocations in `Update` — use caching/pooling if still readable
- Keep physics in `FixedUpdate`; visuals and input in `Update` or `LateUpdate`
- `ScriptableObject` for tunable parameters — runtime state separate from config
- Keep scene load boundaries explicit; use a bootstrap scene for wiring systems

---

## 8. Debugging & Diagnostics

- Prefer scoped `ILogger`-style wrappers or conditional logs
- Include board snapshots or FEN strings in failure messages where useful
- Make error messages actionable:

  - Example: `"King position not found in BoardState"`

- Log with intention — avoid noise that hides real issues

---

## 9. Git LFS & Unity Asset Management

- Use Git LFS for large binaries
- Textures, audio files, 3D models, large prefabs, and scene files should be stored using Git LFS to avoid bloating the repository.
- Keep Library/ out of source control
- Unity will regenerate this folder automatically — committing it will cause unnecessary merge conflicts and repo size issues.
- Scene and prefab changes should be intentional
- Avoid noisy edits by disabling auto-save when reviewing changes.
- Stage and commit only the modified assets that are relevant to your change.

Explicit Warning:

DO NOT RUN git commit COMMANDS — ONLY EVER REPLY WITH YOUR RECOMMENDED GIT MESSAGE.
All commits will be reviewed and executed manually.

---

**Quick Starter Checklist for Claude:**

1. Confirm Unity project opens in VS Code, solution and csproj generated
2. Confirm `.asmdef` boundaries for Core, Gameplay, Presentation, Tests
3. Run EditMode tests, then PlayMode tests
4. Add or adjust tests before code changes
5. Output staged files, commit message, and “what & why”, then pause

> Think of scenes as levels and Core as the rulebook. Actors on stage follow the rulebook — they don’t rewrite it mid-performance.

---
