Light Golem Provider
====================

Hi-Level Agent


## Założenia:

* Szybkie i proste sprzedawanie WASM processing.
* Płatnosć tylko za CPU + czas.
* zdefiniowany maksymalny czas i budrzet.

## Moduły

### MarketStrategy

Docelowo fasada. Na start prosty algorytm.

Odpowiada za:

* Ceny
* Reputację
* Scoring zamówień

### EnvManActivator

Docelowo fasada. Na start tylko WASM.

Odpowiada za: 
* Techniczną cześć oferty
* Za uruchomienie środowika uruchomieniowego. 
* Za odczyt stanu: Działa / Nie działa

### Configuration

Generalna konfiguracja.

## Techniczne założenia

Kontrowersyjne:
* Każdy moduł odpowiada za trwałość swoich danych.

## Struktura Oferty

```yaml
golem:
    node:                    # <- Configuration
      id:
        name: node Name
      inf:                   # <- EnvManActivator
        cpu:
          architecture: wasm32
          bit: ["32"]
          cores: 1           #  każdy core jest sprzedawany oddzielnie
          threads: 1
        mem:
          gib: 0.5           #  efektywnie wasm32 może adresować mniej niż 2GB. 
        storage:
          gib: 500
        runtime:
          wasm:
            wasi:
              version@v:  0.1.0
              caps: ["fs"]
            emscripten:
              js@v: 9.0.0   # ECMAScript 2018
              caps: ["fs"]
       srv:
        comp:
          wasm:
            task_package: *

```

```yaml
properties:
    - golem.srv.comp.srv.wasm.benchmark{*} 
```

## Algorytm agenta

### publish_offer

Wołane zawsze przy zmianie konfiguracji któregoś z modułów.

1. Generowanie Szablonu oferty `Configuration::define_offer`
2. Generowanie Ofert dla środowisk uruchomieniowych: `EnvManActivator::decorate_offer`
3. Dla każdej oferty:
    1. Dodanie warunków komercyjnych do ofert: `MarketStrategy::decorate_offer`
    2. uruchomienie aktora OfferController dla danej subskrypcji i danej referencji do EnvManActivator
     
### OfferController

#### Stan:
* WaitForDemands
* Working

#### WaitForDemands

Pobierane są zlecenia i dodawane do kolejki otwartych zleceń.

W odstępach czasu:
* wywalenie przeterminowanych żądań z kolejki otwartych


Agent:

1. EnvMan.GetOffers
2. Configuration.getNode -> "golem.node"
3. call MarketStrategy.Decorate(offer)
4. api.subscribe(offer)
5. loop:
    1. api.getDemands
    2. MarketStrategy.Score(demands)
    3. todo
6. todo

    
MarketStrategy:

1. EnvMan (fasada)- GetOffers Gener

Configuration:

1. GET/SET1. SUBSCRIBE
