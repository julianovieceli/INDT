-- Executar

docker compose -f docker-compose-sbus-emulator.yml down
docker compose -f docker-compose-sbus-emulator.yml up -d

docker compose -f docker-compose-mysql.yml down
docker compose -f docker-compose-mysql.yml up -d


docker compose -f docker-compose-MongoDb.yml down
docker compose -f docker-compose-MongoDb.yml up -d

docker compose -f docker-compose-localstack.yml down
docker compose -f docker-compose-localstack.yml up -d

docker compose -f docker-compose-azurite.yml down
docker compose -f docker-compose-azurite.yml up -d


