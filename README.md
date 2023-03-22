# Chatify

Klone das Repository mit folgendem Befehl:

```
git clone https://github.com/IlyaFreydkin/Chatify.git
```

## Kurzbeschreibung

Chatify ist eine Single Page Application zum live Video chatten


## Teammitglieder

| Name                   | Email                    | Aufgabenbereich |
| ---------------------- | ------------------------ | --------------- |
| Ilya *Freydkin*, 3CHIF | fre22343@spengergasse.at |                 |
| Richard *Liu*, 3CHIF   | liu2291@spengergasse.at  |                 |
| Uros *Veljic*, 3CHIF   | vel22675@spengergasse.at |                 |
| Janus *Messner*, 3CHIF | mes22377@spengergasse.at |                 |
| Mohamed *Ahmed*, 3CHIF | ahm22106@spengergasse.at |                 |

## Voraussetzungen

Das Projekt verwendet .NET in der Version >= 6 und Docker. Prüfe mit folgendem Befehl, ob die .NET
SDK in der Version 6 oder 7 und Docker am Rechner installiert sind:

```
dotnet --version
docker --version
```

## Backend einrichten

###
```
docker run -d -p 13306:3306 --name chatify_mariadb -e MARIADB_USER=root -e MARIADB_ROOT_PASSWORD=Password mariadb:10.10.3
```
### appsettings.Development.json

Lege im Verzeichnis *tschess/tschess.Backend* die Datei *appsettings.Development.json* an.

- Generiere ein Secret auf https://generate.plus/en/base64 mit 128 Bytes Länge und schreibe es in *JwtSecret*.
- Schreibe deinen AD User in *Searchuser* und *Searchpass*. Die Datei *appsettings.Development.json*
  wird deswegen nicht in das Repository übertragen.
- Lokale Admins können *mit , getrennt* (kein Array) hinterlegt werden. Diese Account bekommen die
  Rolle *Management* im JWT.

```javascript
{
  "ConnectionStrings": {
    "MySql": "server=localhost;port=13306;database=Chatify;user=root;password=Password"
  },
  "Searchuser": "",
  "Searchpass": "",
  "JwtSecret": "",
  "LocalAdmins": "",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Controller für die AD Authentifizierung

**POST /api/user/login** mit `{"username": "myUser", "password": "myPassword"}` liefert ein JSON
Object mit der User ID und dem Token zurück. Wurde ein Suchuser hinterlegt, wird im Development
Mode das Passwort nicht validiert und es werden die Daten des anderen Users zurückgegeben.

**GET /api/user/me** braucht im Header den Bearer Token. Die Route liefert die gespeicherten
Daten im Token zurück (Rolle, Klasse, ...)

## Starten des Servers

Das Programm verwendet ein SQL Server Docker Image für die Speicherung der Daten. Es wird beim
Programmstart automatisch geladen.

**Windows**
```
startServer.cmd
```

**macOs, Linux**
```
chmod a+x startServer.sh
./startServer.sh
```

