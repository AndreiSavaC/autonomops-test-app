using AutonomOpsTestApp;

var svc = new DeploymentService();

var result = svc.Deploy("api-gateway", "v2.1.0");
Console.WriteLine(result.IsSuccess
    ? $"[OK] {result.Message}"
    : $"[FAIL] {result.Message}");

var health = svc.CheckHealth("api-gateway", uptimeSeconds: 60);
Console.WriteLine($"[HEALTH] {health.ServiceName} -> {health.Status}: {health.Message}");
