# Opgave 1: Mandelbrot fractaal applicatie

### Functies


##### Logic.cs / CalculateMandelbrotDepthAsync()

- Deze functie maakt een 2 dimentionale array van getallen aan waarin voor elk punt in de mandelbrot berekend wordt na hoeveel keer het zal difergeren.
  De functie maakt gebruik van de struct 'ComplexNumber' dat de + en - operator bevat. 


##### MainViewModel.cs / GenerateBitmapAsync()

- Om de zware berekeningen in de achtergrond te laten lopen wordt de functie uit de logica in een Task gestopt.
  Na het berekenen van de mandelbrot dieptes worden deze resultaten in een dubbele for lus overlopen en een kleur gegeven.
  Deze dubbele array van kleuren wordt daarna aan de bitmap gegeven om deze te renderen.


##### MainViewModel.cs / GetColor()

- Om elke mandelbrot waarde een kleur te geven wordt er gecontroleerd welke selectie de gebruiker gemaakt heeft in de combobox.
  De mogelijke kleurfilters zijn gedefinieerd in een enum. In de functie wordt er voor elke kleurfilter een kleur teruggestuurd aan de hand van een RGB  waarde. Bij banding wordt de kleur zwart bij even en wit bij oneven. Bij grayscale en multicolor wordt de mandelbrot waarde vermenigvuldigd met 255 en daarna gedeeld door een vaste waarde en in het geval van grayscale niet.


##### MainWindow.xaml.cs / MdbImage_MouseWheel()

- Wanneer de Delta verschil waarde groter dan 0 is wordt de zoomscale maal 40% gedaan, bij uitzoomen word deze gedeeld door 40%;

##### MainWindow.xaml.cs / MdbImage_MouseMove()

- Deze mouse action handler controleerd de vorige positie tegenover de huidige en bepaald of de muis naar links, rechts, boven en of onder is bewogen.
  Voor elke positie wordt er daarna een x en y offset berekend dat wordt toegevoegd aan de berekening van de mandelbrot.
