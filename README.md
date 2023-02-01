# Chatify

Klone das Repository mit folgendem Befehl:

```
git clone https://github.com/IlyaFreydkin/Chatify.git
```

## Kurzbeschreibung

Chatify ist eine Single Page Application zum live Video chatten




## Teammitglieder

| Name                    | Email                  | Aufgabenbereich                         |
| ----------------------- | ---------------------- | --------------------------------------- |
| Ilya *Freydkin*, 3CHIF | fre22343@spengergasse.at | |
| Richard *Liu*, 3CHIF | liu2291@spengergasse.at | |
| Uros *Veljic*, 3CHIF | vel22675@spengergasse.at |  |
| Janus *Messner*, 3CHIF | mes22377@spengergasse.at |  |
| Mohamed *Ahmed*, 3CHIF | ahm22106@spengergasse.at | |

## Voraussetzungen

Das Projekt verwendet .NET in der Version >= 6. Prüfe mit folgendem Befehl, ob die .NET SDK in der
Version 6 oder 7 am Rechner installiert ist:

```
dotnet --version
```

Die .NET 6 SDK (LTS Version) kann von https://dotnet.microsoft.com/en-us/download/dotnet/6.0 für alle
Plattformen geladen werden.

Zum Prüfen der Docker Installation kann der folgende Befehl verwendet werden. Er muss die Version
zurückgeben:

```
docker --version
```

Im Startskript wird der Container geladen, bevor der Server gestartet wird.

## Starten des Programmes

Führe nach dem Klonen im Terminal den folgenden Befehl aus, um den Server zu starten.

**Windows**

```
startServer.cmd
```

**macOS, Linux**

```
chmod 777 startServer.sh
./startServer.sh
```

Nach dem Starten des Servers kann im Browser die Seite **http://localhost:5000**
aufgerufen werden. Falls die Meldung erscheint, dass das Zertifikat nicht geprüft werden kann,
muss mit *Fortsetzen* bestätigt werden.

## Info für Mitglieder
 
Führe den Befehl aus, um eine Docker zu erstellen
```
docker run --name mariadb_chatify -d -p 13307:3306 -e MARIADB_USER=root -e MARIADB_ROOT_PASSWORD=password mariadb:10.10.2
```
(Hinweis: Bei Dbeaver darf bei Name nichts stehen)
