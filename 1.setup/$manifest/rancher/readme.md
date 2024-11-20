# Setup rancher

```
helm repo add rancher-latest https://releases.rancher.com/server-charts/latest
```

```
helm install rancher rancher-latest/rancher \
   --namespace cattle-system \
   --create-namespace \
   --values=values.yaml
```

```
kubectl apply -f svc.yaml
```