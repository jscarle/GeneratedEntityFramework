# Agent Instructions

## Release Intent, Versioning, and Workflow

Treat explicit release requests—such as “Mint a new release,” “Release this,” “Publish this as a new release,” “Create a new release,” or any clear equivalent—as authorization to carry out GeneratedEntityFramework's full release workflow, including committing, pushing, tagging, and publishing as needed.

If the user has not clearly requested a release, or if the appropriate change category cannot be determined confidently, ask for clarification **before** committing, pushing, creating a tag, or publishing a release.

Only change GeneratedEntityFramework's package, assembly, and file versions when the user has clearly requested that a release be published. Do not bump versions during ordinary implementation, refactoring, bug-fixing, dependency updates, testing, commits, pushes, or pull-request preparation unless those actions are part of an explicitly requested release.

Choose the next version using these repository-specific rules:

- Minor, backward-compatible changes increment the patch component. Example: `8.0.2` → `8.0.3`.
- Moderate or breaking changes increment the middle component and reset the patch. Example: `8.0.2` → `8.1.0`.
- The major component matches the newest supported EF Core/.NET generation: EF Core 9 uses `9.x`, EF Core 10 uses `10.x`, and EF Core 11 uses `11.x`. When adding a generation, set the major component to that generation and reset the other components. If generations are skipped, skip the corresponding major versions too; do not merely increment the previous major by one.
- Breaking changes alone do not justify a major-version increment in this repository. If multiple categories apply, use the highest applicable category.

Update `Version` in `src/GeneratedEntityFramework/GeneratedEntityFramework.csproj` using three components and update `AssemblyVersion` and `FileVersion` using the matching four-component form. Use a three-component lightweight tag such as `v8.0.3`; some historical tags are annotated, but new release tags must be lightweight.

A clear release request authorizes this complete workflow:

1. Inspect the worktree, current branch, `origin`, the current default branch (`develop`), recent commits, pull requests, and releases before editing release metadata.
2. Determine the next version, update the three version properties in `src/GeneratedEntityFramework/GeneratedEntityFramework.csproj`, and verify that the tag and release do not already exist.
3. Validate the release-scoped changes using the repository's normal restore, Release build, test, and pack commands.
4. Create or reuse an appropriate `feature/<short-description>` or `feature/release-X-Y-Z` branch; do not commit release work directly to `develop`.
5. Commit and push all release-scoped changes using the repository's recent concise title style.
6. Open a ready pull request against `develop`, use recent pull-request conventions, enable squash auto-merge, and monitor checks and review state.
7. Fix and push any issue that prevents the pull request from merging, then wait until it is merged.
8. Fast-forward local `develop` from `origin/develop`, prune deleted remote refs, and remove the merged local release branch when appropriate.
9. Create the lightweight `vX.Y.Z` tag on the latest `develop` commit and push that tag to `origin`.
10. Publish a non-draft GitHub release named `vX.Y.Z - Description`, using concise release-note bullets consistent with recent GeneratedEntityFramework releases.
11. Monitor the release-triggered `Publish` workflow until the GeneratedEntityFramework NuGet package is published successfully, fixing in-scope failures when possible.

Do not require separate confirmation for each Git or GitHub step after an unambiguous release command. Report the pull request, merge commit, tag, GitHub release URL, and NuGet publishing result when complete.
