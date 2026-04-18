# autonomops-test-app

A minimal .NET 9 console app used as the **Pipeline Agent test target** for [AutonomOps](https://github.com/your-org/autonomops).

The CI pipeline (build → test → docker build+push) is designed with injectable failure scenarios to validate the Pipeline Agent's autonomous fix loop.

## Structure

```
src/AutonomOpsTestApp/       ← Console app with DeploymentService
tests/AutonomOpsTestApp.Tests/ ← xUnit tests (9 tests)
.github/workflows/ci.yml     ← CI: build → test → docker
break/README.md              ← How to inject failure scenarios
Dockerfile
```

## Running locally

```bash
dotnet test        # run all tests
dotnet run --project src/AutonomOpsTestApp
```

## CI Setup

Set the following **repository variable** in GitHub → Settings → Variables → Actions:

| Variable        | Value                |
| --------------- | -------------------- |
| `REGISTRY_HOST` | `<your-LAN-IP>:5000` |

## Injecting failures

See [break/README.md](break/README.md) for step-by-step instructions to reproduce:

- NuGet version conflict
- Failing unit test
- Invalid workflow YAML
- Missing environment variable
- Docker build error
