# Deploy metrics-server on Windows Docker Desktop

```bash
kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml

kubectl delete deployment metrics-server -n kube-system

kubectl apply -f '1.setup/windows-host/metrics-server/deployment.yaml'
```