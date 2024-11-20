
## Commands

```bash
docker compose build

docker compose up -d

docker compose push

# 60 MB * 5 = 300 MB in 30s
for i in {1..30}; do
  echo "Batch $i"
  for j in {1..5}; do
    curl http://api.enclave-it.click/views/count &
  done
  sleep 2
done

```