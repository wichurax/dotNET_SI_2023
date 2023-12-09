# Getting started

Poniżej znajduje się mała instrukcja jak postawić wszystkie niezbędne składniki, aby móc lokalnie symulować pracę systemu
<br />

## Uruchamianie
Wszystkie komponenty docelowo umieszczone będą w osobnych kontenerach Dockera i porozumiewać się pomiędzy sobą poprzez sieć zdefiniowaną w pliku docker-compose.yml.

Uruchamianie aplikacji:
```bash
docker-compose build
docker-compose up
```

## Frontend
Warsta prezentacji została zaimplementowana korzystając z framework'a Angular. Po uruchomieniu aplikacji widok dostępny jest pod http://localhost:4200/.


## MQTT
Aby mieć możliwość wysyłania i odbierania wiadomości należy uruchomić HiveMQ na swoim lokalnym komputerze.
Można zrobić to korzystacjąc z Dokera (rekomendowana opcja)

Przykładowa komenda:
```bash
docker run --name hivemq -p 8080:8080 -p 1883:1883 hivemq/hivemq4
```

Więcej info na stronie https://www.hivemq.com/downloads/docker/ 
<br />

## Baza danych
Aby mieć możliwość zapisu danych do bazy danych należy uruchomić serwer MongoDB na swoim lokalnym komputerze.
Można zrobić to korzystacjąc z Dokera (rekomendowana opcja)

Przykładowa komenda:
```bash
docker run --name mongodb -d -p 27017:27017 mongodb/mongodb-community-server:6.0-ubi8
```

Więcej info na stronie https://www.mongodb.com/compatibility/docker
<br />

## Symulowanie czujników
TODO MS
