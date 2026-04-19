namespace AutonomOpsTestApp;

public class DeploymentService
{
    public DeploymentResult Deploy(string serviceName, string version)
    {
        if (string.IsNullOrWhiteSpace(serviceName))
            return DeploymentResult.Failure("Service name cannot be empty.");

        if (string.IsNullOrWhiteSpace(version))
            return DeploymentResult.Failure("Version cannot be empty.");

        if (!version.StartsWith('v'))
            return DeploymentResult.Failure($"Version '{version}' must start with 'v' (e.g. v1.0.0).");

        return DeploymentResult.Success($"Deployed {serviceName}:{version}");
    }

    public HealthCheckResult CheckHealth(string serviceName, int uptimeSeconds)
    {
        if (uptimeSeconds < 0)
            return new HealthCheckResult(serviceName, Status.Unknown, "Uptime cannot be negative.");

        if (uptimeSeconds == 0)
            return new HealthCheckResult(serviceName, Status.Starting, "Service is starting.");

        if (uptimeSeconds < 30)
            return new HealthCheckResult(serviceName, Status.Degraded, "Service is still warming up.");

        return new HealthCheckResult(serviceName, Status.Healthy, "Service is healthy.");
    }
}

public record DeploymentResult(bool IsSuccess, string Message)
{
    public static DeploymentResult Success(string message) => new(true, message);
    public static DeploymentResult Failure(string message) => new(false, message);
}

public record HealthCheckResult(string ServiceName, Status Status, string Message);

public enum Status { Unknown, Starting, Degraded, Healthy }
namespace AutonomOpsTestApp;

public class DeploymentService
{
  public DeploymentResult Deploy(string serviceName, string version)
  {
    if (string.IsNullOrWhiteSpace(serviceName))
      return DeploymentResult.Failure("Service name cannot be empty.");

    if (string.IsNullOrWhiteSpace(version))
      return DeploymentResult.Failure("Version cannot be empty.");

    if (!version.StartsWith('v'))
      return DeploymentResult.Failure($"Version '{version}' must start with 'v' (e.g. v1.0.0).");

    return DeploymentResult.Success($"Deployed {serviceName}:{version}");
  }

  public HealthCheckResult CheckHealth(string serviceName, int uptimeSeconds)
  {
    if (uptimeSeconds < 0)
      return new HealthCheckResult(serviceName, Status.Unknown, "Uptime cannot be negative.");

    if (uptimeSeconds == 0)
      return new HealthCheckResult(serviceName, Status.Starting, "Service is starting.");

    if (uptimeSeconds < 30)
      return new HealthCheckResult(serviceName, Status.Degraded, "Service is still warming up.");

    return new HealthCheckResult(serviceName, Status.Healthy, "Service is healthy.");
  }
}

public record DeploymentResult(bool IsSuccess, string Message)
{
  public static DeploymentResult Success(string message) => new(true, message);
  public static DeploymentResult Failure(string message) => new(false, message);
}

public record HealthCheckResult(string ServiceName, Status Status, string Message);

public enum Status { Unknown, Starting, Degraded, Healthy }
