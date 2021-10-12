# Opgave 1: Mandelbrot fractaal applicatie

### Functies


##### Logic.cs / ```CalculateMandelbrotDepthAsync()```

- Deze functie maakt een 2 dimentionale array van getallen aan waarin voor elk punt in de mandelbrot berekend wordt na hoeveel keer het zal difergeren.
  De waarde a en b worden aangepast volgens de zoom schaal, x en y offset en de schaal waarden, deze waarden zorgen ervoor dat de a en b waarden specifieker worden aangepast om een bepaald gebied te genereren.


##### MainViewModel.cs / ```GenerateBitmapAsync()```

- Om de zware berekeningen in de achtergrond te laten lopen wordt de functie uit de logica in een Task gestopt met een cancel token zodat bij het herstarten van de taak deze de oude tasks stopt.
  Na het berekenen van de mandelbrot dieptes worden deze resultaten in een dubbele for lus overlopen en een kleur gegeven.
  Deze dubbele array van kleuren wordt daarna aan de bitmap doorgegeven om deze te renderen in een image.


##### MainViewModel.cs / ```GetColor()```

- Om elke mandelbrot waarde een kleur te geven wordt er gecontroleerd welke selectie de gebruiker gemaakt heeft in de combobox.
  De mogelijke kleurfilters zijn gedefinieerd in een enum. In de functie wordt er voor elke kleurfilter een kleur teruggestuurd aan de hand van een RGB  waarde.

  **De mogelijke kleurfilters zijn:**
  1. Banding
  2. Grayscale
  3. Multicolor

+ Bij banding wordt de kleur zwart bij even en wit bij oneven.
+ Bij grayscale wordt de mandelbrot waarde vermenigvuldigd met 255.
+ Bij multicolor worden de kleuren vermenigvuldigd met 255 en daarna gedeeld door een zelf gekozen getal (dit getal veranderen zal de kleuren palet veranderen).


##### ComplexNumber.cs / ```ComplexNumber()```

- Om de mandelbrot te berekenen kan een complex getal worden voorgesteld met de ComplexNumber struct.
  De struct bevat een plus en maal operator dat de 2 complexe getallen optelt of vermenigvuldigd. 
  De ```Norm()``` functie stuurt een double integer getal terug dat de lengte van een vector weergeeft.

##### MainWindow.xaml.cs / ```MdbImage_MouseWheel()```

- Wanneer de ```e.Delta``` waarde groter dan 0 is wordt de zoomscale maal 40% gedaan, bij uitzoomen word deze gedeeld door 40%.

##### MainWindow.xaml.cs / ```MdbImage_MouseMove()```

- Deze mouse action handler controleerd de vorige positie tegenover de huidige en bepaald of de muis naar links, rechts, boven en of onder is bewogen.
  Voor elke positie wordt er daarna een x en y offset berekend dat wordt toegevoegd aan de berekening van de mandelbrot.
