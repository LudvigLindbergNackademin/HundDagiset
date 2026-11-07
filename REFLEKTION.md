Identifierade fel: 
ConsoleMenu.cs:
I public void Run ändrade jag I WriteLine så att dem var numrerade rätt samt lade till WriteLines för att göra det änklare/skönare för ögenen att läsa i Consolen
lade till en break i case "0" och ändrade //default till default

I private void CheckIn ändrade jag så att Size blev size

I private void CheckOut gjorde jag en sats som läser av ifall ID:t är ett giltigt och existerar, om man skriver in ett ID som inte är med i data basen så åker man tillbaka till starten

I private void ShowAvailableCapacity gjorde jag WriteLines som skriver ut Använd, Max, & Ledig Kapacitet. Detta var det svåraste då jag först testade decimal.Round och fick till det så att det stod 3 istället för 3,0 men om jag lade till en liten eller stor hund blev det ingen decimal pungt, t.ex 5.5 = 5. lyckades efter flera trial & error med dogService.XCapacity:0.#

I private void ShowDogs ändrades DebugLine(Unity) till WriteLine(VS) och dogService.Dogs.Length till dogService.Dogs.Count, i foreach (dog dog...) ändrades till (Dog dog...) det gjorde att alla variabler I foreach fungerade

DogService.cs:
I public class Dog gjorde jag ID { get } till ID { get; }. Sedan i public Dog gorde jag name = name till Name = name

I public decimal UsedCapacity gjorde jag int total till decimal total

I public DogService(decimal maxCapacity) gjorde jag maxCapacity = maxCapacity till MaxCapacity = maxCapacity

I public bool CheckIn ändrade jag if (String.IsNullOrWhiteSpace(Name)) till if (String.IsNullOrWhiteSpace(name)), lade till ! i size =, dog dog blev Dog dog, nextId-- blev nextId++

I public bool CheckOut gjorde jag if (dog != null) till if (dog == null)

I public decimal GetLoadPercentage tog jag bara bort ena /

Program.cs
dogservice dogService = blev Dogservice dogService =
menu.run(); ska vara menu.Run();

Testning av lösning:
När jag tästade den slutliga lösningen gick jag igenom först så att kapasiteten visar rätt tal när talen är heltal och när det inehåller ett deimal tal, sedan fyllde jag upp kapasiteten för att se om man stoppas från att lägga till utligare en hund vilket fungerar. Jag testate också vad som händer om man väljer att checka ut ett ID som inte existerar, när man gör det så försviner kapasiteten som hunden har, om du tar bort en stor hund med full kapasitet så kan du lägga till 3 små eller 1 mellan & 1 liten hund, om ingen hund är på dagiset och du skriver in ett ID så kommer det upp att hunden inte hittades och inte så att det krashar. Varje gång jag har testat lösningen har den aldrig krashat, under varje körning av programet har man kunnat trycka på 0 för att stänga programmet så länge man är i start menyn. även om man trycker enter i en tom ruta krashar inte programmet.

Från kompleterings tillfälle 2: Matsystem i HundDagiset

Jag har vidareutvecklat HundDagiset genom att lägga till ett matsystem som håller reda på hundarnas foder och hur mycket mat som finns kvar i lagret.

Jag har lagt till funktioner för att registrera nya fodersorter, ange vilket foder varje hund ska ha och visa hur många portioner som finns kvar. Det går även att mata hundarna och se vilka som har fått mat respektive inte har fått mat.

I Dog har jag lagt till egenskaperna FoodName och HasEaten för att spara hundens fodersort och matstatus. I DogService har jag använt en Dictionary för att hålla reda på fodersorterna och antalet portioner. Jag har även lagt till kontroller som ser till att rätt foder finns i lagret innan en hund matas. När en hund får mat minskar lagret automatiskt med en portion.

I ConsoleMenu har jag lagt till nya menyval så att användaren enkelt kan hantera foderlagret, ange hundarnas foder, mata hundarna och kontrollera deras matstatus.

Genom detta har jag fått en mer utvecklad applikation där hundarnas matbehov och foderlagret hanteras tillsammans med de tidigare funktionerna för in- och utcheckning.