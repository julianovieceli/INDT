-- Executar

docker compose -f docker-compose-sbus-emulator.yml down
docker compose -f docker-compose-sbus-emulator.yml up -d

docker compose -f docker-compose-mysql.yml down
docker compose -f docker-compose-mysql.yml up -d


