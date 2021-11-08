
## Feedback Applied Programming

### Opgave 2: Pendulum Wave

#### Algemeen

#### Architectuur (15%)


***Modulair, meerlagenmodel***

- [x] *Meerlagenmodel via mappen of klassebibliotheken*
- [x] *Dependency injection*
- [x] *Gebruik  MVVM Design pattern*

***'Separation of concern'***

- [ ] *Domein-logica beperkt tot logische laag*
- [ ] *Logische laag onafhankelijk van presentatielaag*


Beperkte logica waardoor deel2 nog niet zinvol beoordeeld kan worden


#### Programmeerstijl, Kwaliteit van de code (10%)

***Naamgeving***

- [ ] *Naamgeving volgens C# conventie*
- [ ] *Zinvolle, duidelijke namen*

***Korte methodes***

- [ ] *maximale lengte ~20 lijnen*

***Programmeerstijl***

- [ ] *Layout code*
- [ ] *Zinvolle, duidelijke namen*
- [ ] *Correct gebruik commentaar*
- [ ] *Algemene programmeerstijl*

Beperkte eigen code waardoor stijl nog niet zinvol beoordeeld kan worden

#### User interface, functionaliteit, UX (15%) 


***Ergonomie***

- [ ] *Layout UI*
- [ ] *estetische weergave* 
- [ ] *Goede UX*

***functionaliteit***

- [ ] *Goede weergave view met controllerbare camera*
- [ ] *Goede weergave 'Bottom' view*
- [ ] *Weergave numerieke resultaten*
- [ ] *Instelbaar aantal slingers*
- [ ] *Instelbare kleurenweergave*
- [ ] *Start, Pauze, Reset*

Huidige functionaliteit van de toepassing: tonen en bewegen van ballen evenwijdig met Z-as in 3D-vlak.
Via 'voeg bal toe' knop extra bal toevoegen verder in de Z-as richting
Via 'beweeg' knop de ballen laten bewegen evenwijdig met de Z-as
Via 'reset' knop ballen laten verdwijnen

geen lopende simulatie

#### Goede werking, snelheid, bugs (25%)


***juiste technieken gebruikt***

- [ ] *Correcte simulatie slinger (o.a. formules)*
- [ ] *Correcte berekening lengte van de slingers (o.a. formule)*
- [ ] *Maximale simulatiefrekwentie*
- [ ] *Realistische renderfrekwentie*

***Juiste werking***

- [ ] *Goede werking*

***Snelheid, efficiëntie, concurrency***

- [ ] *Zinvol gebruik concurrency*
- [ ] *Efficiënte berekeningen*

***Bugs***

- [ ] *Geen bugs*

kan gezien de beperkte functionaliteit niet beoordeeld worden

#### Installeerbare package voor distributie (10%)

- [x] *Installable package beschikbaar in repo*

Package met certificaat voorzien
geen icoontje

#### Correct gebruik GIT (10%)

- [ ] *Gebruik 'atomaire' commits*
- [ ] *zinvolle commit messages*

Weinig zinvolle commits ivm functionaliteit (cf. voorziene functionaliteit)

#### Rapportering (15%)


- [ ] *Structuur*
- [ ] *Volledigheid*
- [ ] *Technische diepgang*
- [ ] *Professionele stijl*

Markdown document voorzien, beperkte info gezien de beperkte functionaliteit
Structuur is oplijsting van functies (resp. 3 in World.cs en 3 in MVM.cs)
Geen toelichting van de geschreven Model-klassen
Geen toelichting vna de reset methode
class Sphere is leeg?