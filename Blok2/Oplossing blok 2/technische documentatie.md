# Opgave 2: Pendulum wave applicatie

### Functies


##### World.cs / ```AddSphere()```

- Deze functie voegt een Point3D object toe aan de ```SpherePosition()``` met telkens een x, y en z offset. De offset posities voor de sphere wordt in de ```GetBalPosition()``` functie toegevoegd en als Point3D object terug gestuurd.

##### World.cs / ```InitBeam()```

- De staaf (beam) bovenaan de slinger krijgt een vast ankerpunt dat wordt terug gestuurd vanuit de ```GetBarPosition()``` functie. De staaf krijgt ook een hoek dat op -90° is geplaats om op de Z as te liggen. De lengte is ook vast bepaald op 1600 om tot 40 ballen te kunnen toevoegen.

##### World.cs / ```MoveObjects()```

- Om de ballen te verplaatsen wordt er voor elke bal een vector opgeteld bij de huidige positie van de bal. met een for lus wordt elke bal in de SpherePositions overlopen en overschreven met de nieuwe positie.


##### MainviewModel.cs / ```AddSphere()```

- Om een bal toe te voegen wordt er een nieuwe positie in de World classe aangemaakt (addSphere). Om de bal een kleur te geven wordt de brush kleur ingesteld op staal blauw. Met Models3D wordt een bal aangemaakt en om de bal te schalen wordt met ScaleTransform3D de bal een bepaalde grootte gegeven.

##### MainviewModel.cs / ```Move()``` / / ```MoveSpheres()```

- In deze functie wordt elke bal opnieuw een vector3D locatie gegeven om daarna met de ```MoveSpheres()``` functie elke bal opnieuw te transformeren.
De ```MoveSpheres()``` functie vertrekt steeds vanuit het middelpunt (origine) van de wereld. Het middelpunt van de verplaatste bal wordt in de transformatie gebruikt door gebruik te maken van TranslateTransform3D. 
