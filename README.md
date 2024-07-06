Este projeto foi criado para fazer o controle de participante inscritos em eventos ou evento.

Para baixar e usar esse projeto Windows:

1- Clone o repositório: 
Abra um terminal e execute:
**git clone https://github.com/henriquepadua/Eventos_Backend.git**

2- Navegue até o diretório do projeto:
**cd Eventos_Backend**

3- Instalar o .NET SDK: 
Instalar o .NET SDK: Baixe ehttps://dotnet.microsoft.com/pt-br .
Restaurar dependências: **dotnet restore**
Execute o projeto: inicie o backend
**dotnet run**

4- Garanta que o Backend esteja em execução: 
confirme se o servidor backend está em execução e acessível, normalmente em **http://localhost:5000** ou em uma URL semelhante. Você deve conseguir acessar os endpoints da API por meio do seu navegador ou do Postman.

PARA VERIFICAR O BANCO DE DOS NO **SQLITESTUDIO**
Etapas para baixar e instalar o SQLiteStudio
Visite o site do SQLiteStudio:

Acesse o site oficial do SQLiteStudio .
Baixe o SQLiteStudio:

Clique no link **"Download"** no menu superior.
Escolha a versão adequada ao seu sistema operacional (Windows, macOS ou Linux).
Instalar SQLiteStudio:

Janelas:
**Baixe o .ziparquivo ou o .exeinstalador.**
Se você baixou o **.ziparquivo**, extraia-o para um local de sua escolha.
Execute **SQLiteStudio.exe** para iniciar o aplicativo.
**Mac OS:** Baixe o **.dmgarquivo.**
Abra o **.dmgarquivo** e arraste o SQLiteStudio para a pasta Aplicativos.
Execute o SQLiteStudio na pasta Aplicativos.
**Linux**:
Baixe o **.tar.gzarquivo.**
Extraia o conteúdo para um diretório de sua escolha.
Abra um terminal, navegue até o diretório onde você extraiu o SQLiteStudio e execute **./SQLiteStudio**.
Executando o **SQLiteStudio**:

Uma vez instalado, abra o SQLiteStudio.
Você pode começar a criar e gerenciar seus bancos de dados SQLite por meio da interface gráfica amigável.
Usando SQLiteStudio
**Criar um novo banco de dados:**
Abra o SQLiteStudio e clique no menu **"Banco de Dados"**.
Selecione **"Adicionar um banco de dados"** e escolha um local para salvar seu novo arquivo de banco de dados.
Abra um banco de dados existente:

Clique no menu **"Banco de dados"**.
Selecione **"Adicionar um banco de dados"** e navegue até seu arquivo de banco de dados SQLite existente.
Gerencie seu banco de dados:

Você pode criar tabelas, executar consultas SQL e gerenciar a estrutura do seu banco de dados usando as ferramentas fornecidas pelo SQLiteStudio.

Em seguida um vídeo que criei para entrar no banco de dados
Vai aparecer opção **"Arquivo:"** **"c:\dados\Eventos.sqlite"**: 
Vai aparecer opção "**Nome(lista)"**: **"main"**


Baixar e testar projeto com **Linux**:
1 - Instalar Git:
Se o Git não estiver instalado, você pode instalá-lo com o seguinte comando:
**sudo apt-get update
sudo apt-get install git**

2 - Instalar .NET SDK:
Siga as instruções no site oficial do .NET para instalar o .NET SDK no Linux: Instalação do .NET no Linux

Para instalar o .NET SDK no Ubuntu, você pode usar os seguintes comandos:
**wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-7.0**

3- Instalar SQLite:
Para instalar o SQLite no Ubuntu, você pode usar o seguinte comando:
**sudo apt-get install sqlite3**

4- Baixar o Projeto:
Clone o repositório do GitHub para o seu sistema local:
**git clone https://github.com/henriquepadua/Eventos_Backend.git
cd Eventos_Backend**

5 - Restaurar Dependências do Projeto:
Dentro do diretório do projeto, execute o comando abaixo para restaurar todas as dependências necessárias:
**dotnet restore**

6 - Configurar o Banco de Dados:
Crie e configure o banco de dados SQLite necessário para o projeto. O repositório deve conter arquivos de configuração ou scripts para isso. Se não houver um script específico, você pode criar o banco de dados manualmente e usar o SQLiteStudio para gerenciá-lo.

Para criar e usar um banco de dados SQLite:
**sqlite3 eventos.db**

7 - Executar o Projeto:
Execute o projeto usando o comando:
**dotnet run**

Passos Detalhados para Instalar o .NET SDK
1 - Adicionar o Repositório do Microsoft Package:
**wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb**

2 - Instalar o .NET SDK:
**sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-7.0**

Passos Detalhados para Baixar e Executar o Projeto
1 - Clonar o Repositório:
**git clone https://github.com/henriquepadua/Eventos_Backend.git
cd Eventos_Backend**

2 - Restaurar Dependências:
**dotnet restore**

3 - Configurar o Banco de Dados:
Crie o banco de dados SQLite:
**sqlite3 eventos.db**

4 - Executar o Projeto:
**dotnet run**

Dicas Adicionais
Atualizar Dependências:
Certifique-se de atualizar as dependências do projeto regularmente executando:
**dotnet restore**

Solucionar Problemas:
Se encontrar problemas, use o comando **dotnet --info** para verificar a instalação do .NET SDK e solucionar problemas de configuração.
