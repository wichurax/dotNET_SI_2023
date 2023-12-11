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
Na ten moment brakuje zapięcia API w kontener więc po powyższych komendach należy uruchomić program **MessageBroker**.

Następnie można uruchomić skrypt z projektu Sensor1 i zaobserwować w logach pobieranie przez api danych z kolejki i składowanie ich w bazie.

## Frontend
Warsta prezentacji została zaimplementowana korzystając z framework'a Angular. Po uruchomieniu aplikacji widok dostępny jest pod http://localhost:4200/.


## MQTT
Aby mieć możliwość wysyłania i odbierania wiadomości należy uruchomić HiveMQ na swoim lokalnym komputerze.
Po uruchomieniu aplikacji UI HiveMQ dostępne jest pod adresem: http://localhost:8080/.

## Baza danych
Aby mieć możliwość zapisu danych do bazy danych należy uruchomić serwer MongoDB na swoim lokalnym komputerze.
Do połączenia z uruchomioną w kontenerze korzystamy z paczki **MongoDB.Driver**. Przykład połączenia:

```csharp
var service = new MongoDbService("mongodb://localhost:27017", "sensors");
```

## Symulowanie czujników
TODO MS
