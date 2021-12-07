# Compilador para a linguagem Pascal

O presente projeto trata-se de um compilador para a linguagem Pascal desenvolvido com a linguagem C# e implementa desde a analise léxica, a analise sintática, analise semântica e geração do código intermediário. 

## Instalação e execução

Necessário [SDK .NET 5](https://dotnet.microsoft.com/download/dotnet/5.0) e [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) para rodar o projeto.

Após instalados, deve-se seguir os seguintes passos:

Compilar o sistema no Visual Studio e então ele irá ler o arquivo chamado `entrada_texto.txt` que está na pasta raiz do sistema.

O conteúdo desse arquivo pode ser alterado conforme os cases de testes disponíveis na seção abaixo ou alterado conforme demanda.

## Alguns cases para teste

1. Programa com duas variáveis do tipo inteiro e cálculos de soma e subtração;
```
programa teste;
var x, y: inteiro;
inicio
  y := 2 + 5;
  x := 2 - 1336;
  escreva(x);
fim.
```

2. Programa com atribuição de variáveis do mesmo tipo

```
programa teste;
var x, y: inteiro;
inicio
  y := 2 + 5;
  x := y - 1336;
  escreva(x);
fim.
```

3. Programa com erro de atribuição de tipos diferente (inteiro e booleano)

```
programa teste;
var 
x, y: inteiro;
z: booleano;
inicio
  y := z;
  y := 2 + 5;
  x := y - 1336;
  escreva(x);
fim.
```