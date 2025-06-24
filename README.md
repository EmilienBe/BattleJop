# BattleJop

## Mise en place de l'environnement de développement

### 1. Lancement du conteneur PostgreSQL avec Docker Desktop

1. Installer **Docker Desktop** si ce n'est pas déjà fait.
2. Depuis un terminal (ou celui intégré à Docker Desktop), exécuter la commande suivante :

   ```bash
   docker run --name battlejop-postgres -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres

   
3. Se connecter au serveur PostgreSQL avec les informations suivantes :
- Hôte : 0.0.0.0
- Port : 5432
- Utilisateur : postgres
- Mot de passe : mysecretpassword
  
4. Créer une base de données nommée BattleJopDb.

### 2. Initialisation de la base de données

1. Ouvrir PowerShell ou le terminal de Visual Studio.
2. Naviguer jusqu’au projet BattleJop.Api.Infrastructure :
   ```bash
   cd chemin/vers/le/projet/BattleJop.Api.Infrastructure
   
3. Exécuter la commande suivante pour appliquer les migrations Entity Framework :
   ```bash
   dotnet ef database update
