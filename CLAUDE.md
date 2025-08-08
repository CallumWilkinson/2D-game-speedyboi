# Claude Code Configuration for Unity C# Project

## Development Guidelines

- Environment: Windows 11, PowerShell, VS Code, C# Dev Kit
- Unity: 2022.3 LTS line (your project currently 2022.3.21f1)
- Editor: VS Code 1.102.x with Unity and C# extensions
- Language: C# 10 or project default, follow Unity’s supported features
- Style: prefer explicit code and clarity over cleverness or micro performance

### Core C# and Unity Rules

- Always preference Object Orientied Programming where appropriate
- Use `PascalCase` for classes and public members, `camelCase` for privates, `_camelCase` or `m_` prefix is acceptable for serialized privates, choose one and stay consistent
- Avoid ternary operators, use clear `if/else`
- Prefer early returns to flatten control flow
- Keep classes small and focused, one responsibility per type
- Put runtime logic in `MonoBehaviour` only when it needs Unity lifecycle
- Put pure logic in plain C# classes and structs inside an assembly definition
- Use `ScriptableObject` for data configuration and light state, treat it like a read only config where possible
- Use `[SerializeField] private` for inspector wired fields, avoid `public` fields
- Avoid `FindObjectOfType`, `Find`, and string based lookups, wire references via inspector or a small composition root
- Prefer composition over singletons, if a singleton is used then make it explicit and well documented
- Coroutines are fine for frame based flow, use async only for pure C# tasks or I/O that does not touch Unity APIs
- Do not call Unity API from background threads
- Replace magic numbers with named `const` or `static readonly` fields
- Log with intention, `Debug.LogError` for actionable failures, `Debug.LogWarning` for unusual but survivable states

> Analogy: treat `MonoBehaviour` scripts like actors on stage, give them cues and references, keep heavy thinking in backstage classes.

### Error Handling

- Guard clauses for nulls and invalid state
- Throw exceptions only in pure code paths, log and fail gracefully in gameplay paths
- Never swallow exceptions silently
- Prefer small validation helpers that make intent obvious

### Clean Code Principles

- **Descriptive names**: `CalculateLegalMoves`, `BoardState`, `MoveValidator`
- **Small methods**: one verb per method, no deep nesting
- **No duplication**: extract helpers for repeated logic
- **Comments explain intent** only when code cannot, update or delete stale comments
- **No hidden magic**: make data flow obvious with parameters and return values
- **Minimize global state**: pass dependencies, avoid hard singletons
- **Architectural consistency**: if usage and class definitions disagree, fix the architecture not the call site hack

## Unity Testing Guidelines

- Framework: Unity Test Framework with NUnit
- Test types:

  - **EditMode** for pure logic and fast feedback
  - **PlayMode** for integration with scenes, prefabs, and MB lifecycles

- Every new function or logic change needs tests

  - If test is not practical, explain why and what would make it testable

- Rules:

  - Use real types and constructors, avoid faking internals
  - Do not override object internals unless testing that behavior
  - Keep test names behavior focused, for example `ItRejectsMovesThatLeaveKingInCheck`
  - DRY test setup with builders or helpers
  - Prefer deterministic data over mocks, mock external boundaries only

- Example folder layout:

  ```
  Assets/Tests/EditMode/Board/
  Assets/Tests/PlayMode/Integration/
  ```

### TDD Loop

- Red: write a failing EditMode test for one rule, for example en passant validation
- Green: implement minimal logic in `Core` to pass
- Refactor: clean names, extract helpers, keep behavior unchanged
- Repeat with PlayMode tests when scene wiring is involved

## Code Documentation Guidelines

- Prefer self documenting code and names
- Use XML docs only for public types and methods that form a contract
- Use short `// why` comments for non obvious decisions or engine quirks
- Tags:

  - `// TODO:` improvements that do not block correctness
  - `// FIXME:` correctness issues that must be addressed
  - `// HACK:` engine or package workaround that should be removed later

## Git and Assets

- DO NOT RUN GIT COMMIT COMMANDS, ONLY EVER REPLY WITH YOUR RECOMMENDED GIT MESSAGE AND I WILL CHECK IT MANUALLY BEFORE I CHOOSE TO COMMIT OR NOT
- Use Git LFS for large binaries, for example textures, audio, models, large prefabs, scenes
- Keep `Library/` out of source control, commit `Packages/manifest.json` and `packages-lock.json`
- Keep generated files out of the repo unless Unity requires them
- Scene and prefab changes should be intentional, avoid noisy edits by disabling auto save when reviewing

## Commit Guidelines

### Format

```
<type>: <short summary>

<detailed explanation, if needed>
```

**Types**: `feat`, `fix`, `refactor`, `test`, `docs`, `chore`

### Content Rules

- One logical change per commit
- Bundle logic and its tests together when it makes sense
- Each commit must compile and pass tests locally
- Use bullets in the body for multiple points
- Explain why for reversals or risky changes

### Review and Approval Process

- **Never commit automatically**
- After each change, stage only relevant files and output:

  - staged filenames
  - the final commit message
  - a short summary of what changed and why

- Wait for my confirmation before proceeding

#### Example Unity commit output

```
Staged files:
- Assets/Scripts/Core/Board/MoveValidator.cs
- Assets/Tests/EditMode/Board/MoveValidatorTests.cs
- Assets/Scripts/Core/Board/Rules/CheckDetection.cs
- Assets/Scripts/Core/Board/Rules/ICheckRule.cs
```

**Commit message**

```
feat: add check detection and integrate with move validation

- Introduce ICheckRule and CheckDetection for king in check evaluation
- Extend MoveValidator to reject moves that leave king in check
- Add EditMode tests for check scenarios and pinned piece cases
- Keep logic in Core assembly, no UnityEngine dependencies
```

**Why**

- Centralizes check logic behind a small interface, makes rules composable and testable
- Prevents illegal moves early, reduces UI level error handling

## Task Scope and Context Limits

- Large refactors must be split into small steps
- If the full change does not fit, do a small chunk and state what remains, for example “first 3 files done, continue to proceed”

## Refactoring Expectations

- Mark questionable old code with `// legacy:` then explain the risk
- Do not delete unknown code paths without confirmation
- Prefer targeted refactors with tests that lock behavior
- If architecture is unclear, propose a small diagram in text and request sign off

## Unity Specific Practices

- Prefer `Awake` for internal setup, `Start` for cross object wiring that needs other `Awake` calls to have run
- Use `OnEnable` and `OnDisable` to subscribe and unsubscribe events
- Avoid allocations inside `Update`, consider caching and pooling where it keeps code readable
- Keep physics in `FixedUpdate`, keep visuals and input in `Update` or `LateUpdate`
- Use `ScriptableObject` for tunable parameters, keep runtime state separate from config
- Keep scene load boundaries clear, consider a bootstrap scene that wires systems

## AI Commenting Rules

1. Comment only when intent is not obvious
2. Never narrate what obvious code does
3. Explain assumptions, constraints, and engine quirks
4. Flag non obvious behavior and edge cases
5. Use `TODO`, `FIXME`, `HACK` responsibly
6. XML docs for public APIs that other code depends on
7. Delete comments that no longer add value

### Good comment example

```csharp
// Skip first legal move, it is a null move placeholder inserted by the generator
for (int i = 1; i < legalMoves.Count; i++) { ... }
```

## Testing Philosophy, Extended

- Behavior based test names, for example `ItFlagsCheckWhenAttackerHasLineOfSight`
- Minimal fixtures, prefer builders for complex board setups
- Test observable results, not private fields
- Keep EditMode fast and abundant, keep PlayMode focused and few

## Git Hygiene, Extended

- Every commit answers what changed and why
- No broken states in history
- Use present tense in summaries

## Debugging and Diagnostics

- Prefer small, scoped `ILogger` style wrappers if needed, or conditional logs
- Include board snapshots or FEN strings in failure messages when useful
- Make error messages actionable, for example “King position not found in BoardState”

## Change Friendly Design

- Model concepts first, then code
- Design seams where rules can be swapped, for example `IMoveRule`, `ICheckRule`
- Keep dependencies explicit so tests can assemble systems easily

---

### Quick Starter Checklist for Claude

1. Confirm project opens in VS Code from Unity, solution and csproj generated
2. Confirm `.asmdef` boundaries for Core, Gameplay, Presentation, Tests
3. Run EditMode tests, then PlayMode tests
4. Add or adjust tests before code changes
5. Output staged files, commit message, and short why, then pause

> Think of scenes as levels and Core as the rulebook. Actors on stage should read from the rulebook, not write new rules mid performance.
