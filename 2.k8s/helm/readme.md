# Complete Helm Chart for ENC App

## Installation
```bash
# Install chart
helm install enc-app ./enc-app
helm install enc-app ./enc-app --namespace enc-app-dev --create-namespace # Install with namespace enc-app-dev

# Upgrade existing installation
helm upgrade enc-app ./enc-app --namespace enc-app-dev

# Debug the helm chart (render manifest)
helm template enc-app ./enc-app --namespace enc-app-dev --debug

# Uninstall
helm uninstall enc-app
```

## Configuration
The following table lists the configurable parameters of the chart and their default values.

| Parameter | Description | Default |
|-----------|-------------|---------|
| `namespace` | Kubernetes namespace | `enc-app-dev` |
| `global.environment` | Global environment setting | `Docker` |
| `apiGateway.replicas` | Number of API Gateway replicas | `1` |
| `apiGateway.image.repository` | API Gateway image repository | `api-gateway` |
| `apiGateway.image.tag` | API Gateway image tag | `latest` |
| `viewService.replicas` | Number of View Service replicas | `1` |
| `viewService.redis.connection` | Redis connection string | `redis:6379` |
| `redis.persistence.size` | Redis PVC size | `1Gi` |

## Usage Examples

### Custom Values
Create a custom values file `my-values.yaml`:
```yaml
namespace: custom-namespace
apiGateway:
  replicas: 2
  image:
    tag: v1.0.0
```

Install with custom values:
```bash
helm install enc-app ./enc-app -f my-values.yaml
```

### Testing
```bash
# Test template rendering
helm template enc-app ./enc-app

# Test installation
helm install enc-app ./enc-app --dry-run
```

## Development
```bash
# Lint chart
helm lint ./enc-app

# Package chart
helm package ./enc-app

