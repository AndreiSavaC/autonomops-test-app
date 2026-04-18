# Injectable Failure Scenarios

Each scenario below shows exactly what to change to inject a failure, and how to restore.

---

## Scenario 1 — NuGet Version Conflict

**Target**: `dependency_conflict` failure type

Edit `tests/AutonomOpsTestApp.Tests/AutonomOpsTestApp.Tests.csproj`:

```xml
<!-- Change this line: -->
<PackageReference Include="xunit" Version="2.9.3" />
<!-- To: (nonexistent version) -->
<PackageReference Include="xunit" Version="99.0.0" />
```

Expected pipeline step to fail: **Restore dependencies** in the `build` job.

Restore: revert the version back to `2.9.3`.

---

## Scenario 2 — Failing Unit Test

**Target**: `test_failure` failure type

Edit `tests/AutonomOpsTestApp.Tests/DeploymentServiceTests.cs`:

```csharp
// Change this assertion:
Assert.True(result.IsSuccess);
// To:
Assert.False(result.IsSuccess);   // intentionally wrong
```

Expected pipeline step to fail: **Run tests** in the `test` job.

Restore: revert the assertion.

---

## Scenario 3 — Invalid Workflow YAML Syntax

**Target**: `yaml_syntax_error` failure type

Edit `.github/workflows/ci.yml` — introduce a bad indent:

```yaml
  build:
    name: Build
      runs-on: ubuntu-latest   # ← extra indent causes YAML parse error
```

Expected: GitHub rejects the workflow entirely; no jobs run.

Restore: remove the extra indent.

---

## Scenario 4 — Missing Environment Variable

**Target**: `missing_env_var` failure type

Edit `.github/workflows/ci.yml` — reference an undefined variable in the docker job:

```yaml
- name: Push Docker image
  run: |
    docker push $UNDEFINED_VAR/$IMAGE_NAME:latest
```

Expected pipeline step to fail: **Push Docker image** in the `docker` job (empty registry host).

Restore: revert to `$REGISTRY/$IMAGE_NAME:latest`.

---

## Scenario 5 — Docker Build Error

**Target**: `docker_build_error` failure type

Edit `Dockerfile` — reference a file that doesn't exist:

```dockerfile
COPY nonexistent-file.txt /app/
```

Expected pipeline step to fail: **Build Docker image** in the `docker` job.

Restore: remove the COPY line.
