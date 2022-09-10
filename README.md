# .NET-5-REST-API

# Instalação do Projeto Rede Social em NET 5

- Ola, este é um Projeto Rede Social.
- Estamos montando a descrição das funcionalidades.


## 00° Lembre de fazer os apontamentos ao banco de dados que você vai utilizar:
```

Arquivo : appsettings.json / ConnectionStrings em ambos projetos
00 - .\RedeSocialAPI
00 - .\RedeSocialWeb

Nesse Projeto utilizei o Bancos de dados SQL Azure e o BlobStorage para amazenar dados da rede social
para utilizar essa funcionalidade, será necessário criar um acesso na Azure e preenches as variáveis.
Já o Insights você poderá ignora-lo se quiser.
```


## STEPS POWRSHELL

## 1° instalar o dotnet-ef
```
dotnet tool install --global dotnet-ef --version 3.0.0
```

## 2° Atualizar a versão se existir
```
dotnet tool update --global dotnet-ef
```

## 3° Você vai precisar executar o migration em ambos projetos API e MVC
```
00 - cd .\RedeSocialAPI
00 - cd .\RedeSocialWeb
01 - dotnet ef migrations add "init"
02 - dotnet ef database update
```

## Caso queira criar novas tabelas utilize os comandos:

## NUGET-CONSOLE
```
03 - add-migration "addCommentTable"
04 - database-update
05 - add-migration "addPessoaTable"
06 - database-update

```





