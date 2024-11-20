## Apply all manifests

```bash
kubectl apply -f 1.namespace.yaml

kubectl apply -f 2.redis-pvc.yaml
kubectl apply -f 3.redis.yaml

kubectl apply -f 4.view-service.yaml
kubectl apply -f 5.hello-service.yaml
kubectl apply -f 6.api-gateway.yaml

kubectl apply -f 7.ingress.yaml
```

## Checking
kubectl get all -n enc-app-dev


## Monitoring

## Check pods
kubectl get pods -n enc-app-dev

## Check logs of pods
kubectl logs [pod-name] -n enc-app-dev

## Desc a pod
kubectl describe pod [pod-name] -n enc-app-dev

## Check services
kubectl get svc -n enc-app-dev

## Port-forward (locally test)
kubectl port-forward svc/api-gateway 8080:80 -n enc-app-dev

## Remove workload

kubectl delete -f 1.namespace.yaml

<!-- kubectl delete -f 2.redis-pvc.yaml
kubectl delete -f 3.redis.yaml

kubectl delete -f 4.view-service.yaml
kubectl delete -f 5.hello-service.yaml
kubectl delete -f 6.api-gateway.yaml

kubectl delete -f 7.ingress.yaml -->