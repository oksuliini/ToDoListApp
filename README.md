# TodoListApp

## Yleiskatsaus

TodoListApp on Windows Forms -sovellus, joka on kehitetty C#-kielellä tehtävien hallintaan. Sovellus tarjoaa käyttäjille mahdollisuuden luoda, muokata, poistaa ja merkitä tehtäviä suoritetuiksi. Lisäksi se mahdollistaa tehtävien priorisoinnin ja määräaikojen asettamisen. Tehtävät tallennetaan ja ladataan tekstimuotoisesta tiedostosta, mikä varmistaa tietojen säilymisen istuntojen välillä.

## Ominaisuudet

- **Tehtävien Hallinta**: Luo, muokkaa ja poista tehtäviä.
- **Tehtävien Priorisointi**: Aseta tehtäville prioriteetit (Korkea, Keskitaso, Matala).
- **Määräaikojen Muistutukset**: Ilmoita käyttäjälle, kun tehtävän määräaika lähestyy.
- **Tehtävän Status**: Merkitse tehtävät suoritetuiksi ja katso niitä eri väreissä.
- **Tehtävien Suodatus**: Suodata tehtäviä niiden prioriteetin tai suoritetun tilan perusteella.

## Käyttöönotto

Seuraamalla näitä ohjeita pääset alkuun TodoListAppin kanssa:

1. **Kloonaa Repositorio**:
    ```bash
    git clone https://github.com/omaKäyttäjätunnus/TodoListApp.git
    cd TodoListApp
    ```

2. **Avaa Ratkaisu**:
    Avaa `TodoListApp.sln` -tiedosto Visual Studiossa.

3. **Rakenna Ratkaisu**:
    Visual Studiossa, rakenna ratkaisu kääntääksesi sovelluksen.

4. **Käynnistä Sovellus**:
    Käynnistä sovellus Visual Studiossa tai ajamalla käännetty suoritettava tiedosto.

## Sovelluksen Toiminta

### Kuvakaappaukset

![1](https://github.com/user-attachments/assets/1c4a57c8-5eda-48e9-b69e-399ab8942ed6)

*Pääikkuna, jossa näkyvät tehtävät ja tehtävien hallintavaihtoehdot.*

![screenshot1](https://github.com/user-attachments/assets/61e4e6b8-8b66-4c7e-af9a-c579b91deaee)

*Tehtävän lisäys -ikkuna uusien tehtävien luomista varten.*

![screenshot3](https://github.com/user-attachments/assets/88cb9b57-ef31-47e4-874a-56582da5e659)
*Tehtävän muokkaus -ikkuna olemassa olevien tehtävien muokkaamista varten.*

### Vuokaavio

Alla oleva vuokaavio havainnollistaa TodoListAppin pääprosessivirran:

![Vuokaavio](diagrams/flowchart.png)

## Koodin Pääkohdat

### Tehtävien Hallinta

`Form1`-luokka käsittelee tehtävien hallintaa, mukaan lukien niiden lisäämistä, poistamista ja muokkaamista. Tässä on tiivistelmä joistakin tärkeistä metodeista:

- **`AddTask`**: Lisää uuden tehtävän listalle ja päivittää käyttöliittymän.
- **`RemoveTask`**: Poistaa valitun tehtävän listalta ja päivittää tiedostot.
- **`MarkAsDone`**: Merkitsee valitun tehtävän suoritetuksi ja päivittää sen ulkoasun.
- **`LoadTasks`**: Lataa tehtävät tiedostosta ja alustaa tehtävälistan.

### Tiedoston Käsittely

Sovellus tallentaa ja lataa tehtävät tekstimuotoisista tiedostoista:

- **`SaveButton_Click`**: Tallentaa nykyiset tehtävät `tasks.txt` -tiedostoon.
- **`LoadTasks`**: Lukee tehtävät `tasks.txt` -tiedostosta ja päivittää tehtävälistan.

### Käyttöliittymän Piirtäminen

`taskListBox_DrawItem`-metodi mukauttaa tehtävien ulkoasua niiden prioriteetin ja suoritusasteen mukaan:

```csharp
private void taskListBox_DrawItem(object sender, DrawItemEventArgs e)
{
    e.DrawBackground();
    var listBox = sender as ListBox;
    if (listBox != null && e.Index >= 0 && e.Index < listBox.Items.Count)
    {
        var item = listBox.Items[e.Index] as ColoredListBoxItem;
        if (item != null)
        {
            e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(item.Color), e.Bounds);
        }
        else
        {
            e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
    }
    e.DrawFocusRectangle();
}
```

# Tulevat Kehitysideat

## Käyttäjätunnistus
Suunnitteilla on käyttäjien kirjautumisen ja rekisteröitymisen mahdollisuus. Tämä mahdollistaa tehtävien hallinnan käyttäjäkohtaisesti ja synkronoinnin eri laitteiden välillä.

## Parannettu Käyttöliittymä
Tulemme kehittämään käyttöliittymää nykyaikaisilla suunnittelu-elementeillä ja teemoilla. Tavoitteena on parantaa käyttäjäkokemusta ja visuaalista ilmettä.

## Tietokantaintegraatio
Pohdimme tiedostojen käytöstä siirtymistä tietokantaan, kuten SQLite tai SQL Server. Tämä mahdollistaa paremman tehtävien hallinnan ja tehokkaammat kyselyt.

## Tehtäväilmoitukset
Suunnitteilla on järjestelmäilmoituksia tulevista määräajoista ja muistutuksista, jotta käyttäjät eivät unohda tärkeiden tehtävien erääntymisiä.

## Mobiilisovellus
Kehitämme sovelluksesta mobiiliversion, joka mahdollistaa tehtävien hallinnan myös liikkeellä ollessa ja tarjoaa synkronoinnin eri laitteiden välillä.

