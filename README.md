# 3º Projeto de Linguagens de Programação I 2019/2020

## Autoria

**Diogo Heriques (a21802132)**

- Trabalhou no rendering, o início do `FileReader`, `Highscore`, fez o UML,
DoxyGen, e ajudou o Inácio Amerio na parte do cálculo dos vetores para `World`.

**Inácio Amerio (a21803493)**

- Trabalhou na estrutura e ficou encarregado com a organização do projeto.
Trabalhou na criação de todas as classes do projeto de forma a organizar bem o
projeto. Principalmente trabalhou na estrutura de dados do projeto e na lógica
do jogo próprio.

**João Dias (a21803573)**

- Trabalhou em geração procedimental, as opções na invocação do programa,
progressão de níveis, na classe `Game`, `World`, `Agent` e `Program`.

[Repositório Git público.](https://github.com/FPTheFluffyPawed/Project3_LP2019)

## Arquitetura da solução

### Descrição da solução

O programa foi organizado de forma que se possa usar em Unity e em Consola.
Utilizamos o projeto "Zombies Vs Humans" como referencia na criação de este
projeto.

`Program` trata na inicialização do programa, com um método chamado `Options()`
que trata na verificação dos comandos inseridos antes de invocar o programa.

A classe `Game` é utilizada para ligar a classe `Program`. Aqui é que ocorre
tudo relacionado com o jogo próprio, incluindo a lógica do jogo, o _loop_, a
mudança de níveis e a invocação de guardar a pontuação do jogador.

A classe `FileReader` trata da leitura e escrita do ficheiro de pontuações.
Aqui é que se pede a pontuação do jogador e guarda no ficheiro. O ficheiro
próprio é criado ao iniciar o programa.

`EnemyMovement` e `PlayerMovement` são as classes que tratam do movimento para
os inimigos ou jogador. Ambas classes herdam de `AbstractMovement` que força a
estas classes a terem o construtor e o método `WhereToMove()`.

`Direction`, `AgentType`, `Highscore` e `Position` são classes que armazenam
dados. No caso do `Direction`, trata das direções possíveis para movimento,
`AgentType` tem os possíveis tipos de agentes e `Highscore` tem dois campos
para guardar o nome e a pontuação, finalmente, `Position`, é utilizado para
as dimensões de linha e coluna.

`Agent` é a classe que trata dos agentes. Um agente contém a vida atual, a
posição, tipo de movimento e tipo de agente que é. O método `PlayTurn()` trata
do turno de um agente quando é chamado. Esta é a classe que `World` utiliza na
criação de um mundo para jogar.

`World` utiliza `IReadOnlyWorld` para a criação de um mundo em que se pode
meter os agentes. Esta classe tem vários métodos relacionados com o movimento
e verificação de lugares disponíveis, por exemplo, `IsOutOfBounds()` e
`IsOccupied()` são métodos utilizados para a verificação de posições quando
for necessário.

`ConsoleUserInterface` trata na renderização da informação do estado do jogo
atual. Utilizando a classe `Game` e `IReadOnlyWorld` como variáveis de
referencia para fazer _output_ da informação no estado visual.

### Geração procedimental

Para a geração procedimental dos inimigos, fizemos um aumento da percentagem do
inimigo ser um boss consoante o nível, isto é por cada nível que passa a
possibilidade do inimigo ser um boss sobe 10% sendo que no primeiro nível
começa com 10%, isto leva a que a partir do nível 10 o jogador irá sempre
confrontar bosses. Para os power ups atribuímos valores aos tipos médios e
grandes, sendo 5 e 7 respetivamente, isto para rodar um valor random entre
(0, 10 + nível) depois comparamos se o valor destes é superior ou igual ao
random, caso nenhum destes seja então o power up será um pequeno, fazendo assim
com que a possibilidade no nível 1 (que é lido como 0 no random) de ser grande
seja de 50% e a do médio seja 70%, com a progressão dos níveis o valor máximo
do random irá aumentar progressivamente fazendo assim as percentagens de ser
grande ou médio descem. O numero de _powerups_ não é afetado pelo nível mas sim
por um random proporcional ao tamanho do mapa, pondo sempre no mínimo 2 power ups.
 
### Diagrama UML

![UML](UML.png)

### Referências

* A solução do projeto "Zombies Vs Humans" pelo professor, Nuno Fachada.
[Link](https://github.com/VideojogosLusofona/lp1_2018_p2_solucao).
