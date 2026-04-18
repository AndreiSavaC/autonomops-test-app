using AutonomOpsTestApp;

namespace AutonomOpsTestApp.Tests;

public class DeploymentServiceTests
{
  private readonly DeploymentService _svc = new();

  // ── Deploy ──────────────────────────────────────────────────────────────

  [Fact]
  public void Deploy_ValidInput_ReturnsSuccess()
  {
    var result = _svc.Deploy("api-gateway", "v1.0.0");

    Assert.True(result.IsSuccess);
    Assert.Contains("api-gateway", result.Message);
    Assert.Contains("v1.0.0", result.Message);
  }

  [Fact]
  public void Deploy_EmptyServiceName_ReturnsFailure()
  {
    var result = _svc.Deploy("", "v1.0.0");

    Assert.False(result.IsSuccess);
    Assert.Contains("cannot be empty", result.Message);
  }

  [Fact]
  public void Deploy_EmptyVersion_ReturnsFailure()
  {
    var result = _svc.Deploy("api-gateway", "");

    Assert.False(result.IsSuccess);
    Assert.Contains("cannot be empty", result.Message);
  }

  [Fact]
  public void Deploy_VersionWithoutVPrefix_ReturnsFailure()
  {
    var result = _svc.Deploy("api-gateway", "1.0.0");

    Assert.False(result.IsSuccess);
    Assert.Contains("must start with 'v'", result.Message);
  }

  // ── HealthCheck ───────────────────────────────────────────────────────────

  [Fact]
  public void CheckHealth_UptimeOver30s_ReturnsHealthy()
  {
    var result = _svc.CheckHealth("api-gateway", uptimeSeconds: 60);

    Assert.Equal(Status.Healthy, result.Status);
  }

  [Fact]
  public void CheckHealth_UptimeZero_ReturnsStarting()
  {
    var result = _svc.CheckHealth("api-gateway", uptimeSeconds: 0);

    Assert.Equal(Status.Starting, result.Status);
  }

  [Fact]
  public void CheckHealth_UptimeUnder30s_ReturnsDegraded()
  {
    var result = _svc.CheckHealth("api-gateway", uptimeSeconds: 15);

    Assert.Equal(Status.Degraded, result.Status);
  }

  [Fact]
  public void CheckHealth_NegativeUptime_ReturnsUnknown()
  {
    var result = _svc.CheckHealth("api-gateway", uptimeSeconds: -1);

    Assert.Equal(Status.Unknown, result.Status);
  }
}
