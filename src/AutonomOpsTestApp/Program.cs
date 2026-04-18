using AutonomOpsTestApp;

var svc = new DeploymentService();

// Demo: deploy a service and check its health
var result = svc.Deploy("api-gateway", "v2.1.0");
Console.WriteLine(result.IsSuccess
    ? $"[OK] {result.Message}"
    : $"[FAIL] {result.Message}");

var health = svc.CheckHealth("api-gateway", uptimeSeconds: 60);
Console.WriteLine($"[HEALTH] {health.ServiceName} → {health.Status}: {health.Message}");
