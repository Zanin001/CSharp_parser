# Compilador para a linguagem Pascal

O presente projeto trata-se de um compilador para a linguagem Pascal desenvolvido com a linguagem C# e implementa desde a analise l�xica, a analise sint�tica, analise sem�ntica e gera��o do c�digo intermedi�rio. 

## Instala��o e execu��o

Necess�rio [SDK .NET 5](https://dotnet.microsoft.com/download/dotnet/5.0) e [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) para rodar o projeto.

Ap�s instalados, deve-se seguir os seguintes passos:

Compilar o sistema no Visual Studio e ent�o ele ir� solicitar o caminho do arquivo a ser compilado, por exemplo: 

`C:\\TrabalhoA2_Compiladores\\entrada_texto.txt`

O conte�do desse arquivo pode ser alterado conforme os cases de testes dispon�veis na se��o abaixo ou alterado conforme demanda.

Ap�s digitar o caminho e se for v�lido, ele ir� fazer os processos de compila��o e printar os erros ou o c�digo intermedi�rio na tela. Caso ocorra sucesso, o arquivo `llvm.ll` da pasta raiz do programa ser� atualizado com o novo c�digo intermedi�rio.

## Alguns cases para teste

1. Programa com duas vari�veis do tipo inteiro e c�lculos de soma e subtra��o;
```
programa teste;
var x, y: inteiro;
inicio
  y := 2 + 5;
  x := 2 - 1336;
  escreva(x);
fim.
```

2. Programa com atribui��o de vari�veis do mesmo tipo

```
programa teste;
var x, y: inteiro;
inicio
  y := 2 + 5;
  x := y - 1336;
  escreva(x);
fim.
```

3. Programa com erro de atribui��o de tipos diferente (inteiro e booleano)

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