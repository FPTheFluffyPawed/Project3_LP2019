# 3º Projeto de Linguagens de Programação I 2019/2020

## Autoria

**Diogo Heriques (a21802132)**

- .

**Inácio Amerio (a21803493)**

- .

**João Dias (a21803573)**

- .

[Repositório Git público.](https://github.com/FPTheFluffyPawed/Project3_LP2019)

## Arquitetura da solução

### Descrição da solução

### Geração procedimental
Para a geração procedimental dos inimigos, fizemos um aumento da percentagem do inimigo ser um boss consoante o nivel, isto é por cada nivel que passa a possibilidade do inimigo ser um boss sobe 10% sendo que no primeiro nivel começa com 10% de possibilidade, isto leva a que apartir do nivel 10 o jogador irá sempre confrontar bosses. Para os power ups atribuimos valores aos tipos medios e grandes, sendo 5 e 7 respetivamente, isto para rodar um valor random entre (0, 10 + nivel) depois comparamos se o valor destes é superior ou igual ao random, caso nenhum destes seja entao o power up será um pequeno, fazendo assim com que a possibilidade no nivel 1 (que é lido como 0 no random) de ser grande seja de 50% e a do médio seja 70%, com a progressão dos niveis o valor máximo do random irá aumentar progressivamente fazendo assim as percentagens de ser grande ou médio decerem. O numero de powerups não é afetado pelo nivel mas sim por um random propocional ao tamanho do mapa, pondo sempre no minimo 2 power ups.
 
### Diagrama UML



### Referências

* A solução do projeto "Zombies Vs Humans" pelo professor, Nuno Fachada.
[Link](https://github.com/VideojogosLusofona/lp1_2018_p2_solucao).
