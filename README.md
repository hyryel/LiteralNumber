# LiteralNumber  
Permet de transcrire les nombres en lettres.

Supporte les nombres de -999 999 999 999 999 999 à 999 999 999 999 999 999.<br/>
Gère les nombres décimaux avec 3 chiffres après la virgule.<br/>
Supporte les 3 formats littéraux suivants :<br/>
- standard
- <a href='http://www.academie-francaise.fr/questions-de-langue#57_strong-em-nombres-criture-lecture-accord-em-strong'>recommandation de 1990 §2</a>
- monétaire (euros uniquement)

## Examples
#### Format classique
```csharp
int chiffre = -123;
Console.WriteLine(chiffre.ToWord(LiteralNumberFormat.Normal));
//moins cent vingt-trois
```
#### Format suivant la recommandation de 1990 de l'académie Française
```csharp
double chiffre = 1230.25;
Console.WriteLine(chiffre.ToWord(LiteralNumberFormat.Recommandation1990));
//mille-deux-cent-trente virgule vingt-cinq
```
#### Format monétaire
```csharp
decimal chiffre = 123.10;
Console.WriteLine(chiffre.ToWord(LiteralNumberFormat.Money));
//cent vingt-trois euros et dix centimes
```

