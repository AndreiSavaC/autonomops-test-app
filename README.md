# AutonomOps Test App

A minimal .NET 9 application used to validate the **AutonomOps Pipeline Agent**.

## Structure

```
src/AutonomOpsTestApp/     – Console app with DeploymentService
tests/AutonomOpsTestApp.Tests/ – xUnit tests
Dockerfile                 – Multi-stage Docker build
.github/workflows/ci.yml   – CI: build → test → docker
```

## CI Pipeline

| Job | Runs | Expected |
|-----|------|----------|
| Build | `dotnet build` | ✅ passes |
| Test  | `dotnet test`  | ✅ passes |
| Docker Build | `docker build` | ❌ **fails** |

## Active Bug

The `Dockerfile` contains a `COPY` instruction that references a production
config file which was never committed to this repository:

```dockerfile
# BUG: this file does not exist in the repo
COPY config/appsettings.prod.json ./config/
```

**Expected CI error:**
```
failed to solve: failed to read dockerfile:
  COPY failed: file not found in build context or excluded by .dockerignore:
  stat config/appsettings.prod.json: file does not exist
```

**Fix:** Remove (or comment out) the erroneous `COPY` line from `Dockerfile`.

This is a typical DevOps mistake — a developer referenced a local config file
that was never tracked in git.
