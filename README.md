# 🖼️ Processamento de Imagens - Morfologia e Compressão

Uma aplicação desktop em C# Windows Forms para processamento de imagens com foco em operações morfológicas e técnicas de compressão. Desenvolvida como parte do projeto acadêmico TTC-Listas 2º Bimestre.

## 📋 Índice

- [🚀 Funcionalidades](#-funcionalidades)
- [🛠️ Tecnologias Utilizadas](#️-tecnologias-utilizadas)
- [📋 Pré-requisitos](#-pré-requisitos)
- [⚙️ Instalação e Execução](#️-instalação-e-execução)
- [🎯 Como Usar](#-como-usar)
- [🔬 Operações Morfológicas](#-operações-morfológicas)
- [📁 Estrutura do Projeto](#-estrutura-do-projeto)
- [🖼️ Formatos Suportados](#️-formatos-suportados)
- [📚 Conceitos Implementados](#-conceitos-implementados)

## 🚀 Funcionalidades

### 🎨 Conversões Básicas
- **Conversão para Escala de Cinza (DMA)**: Transforma imagens coloridas em escala de cinza usando acesso direto à memória
- **Conversão Preto e Branco (DMA)**: Binarização de imagens com threshold automático
- **Espelhamento Vertical**: Reflexão vertical de imagens

### 🔬 Operações Morfológicas
- **Dilatação**: Expande objetos brancos na imagem
- **Erosão**: Reduz objetos brancos na imagem
- **Abertura**: Erosão seguida de dilatação (remove ruídos pequenos)
- **Fechamento**: Dilatação seguida de erosão (preenche lacunas pequenas)

### 📊 Compressão de Dados
- **Run-Length Coding (RLC)**: Algoritmo de compressão sem perda para imagens binárias

### ⚡ Otimizações
- **Implementações DMA**: Versões otimizadas das operações morfológicas usando Direct Memory Access
- **Elementos Estruturantes Personalizáveis**: Diferentes padrões para operações morfológicas

## 🛠️ Tecnologias Utilizadas

- **Linguagem**: C# (.NET Framework 4.8)
- **Interface**: Windows Forms
- **Processamento de Imagem**: System.Drawing e System.Drawing.Imaging
- **Paradigma**: Programação orientada a objetos com métodos unsafe para otimização

## 📋 Pré-requisitos

- Windows 7 ou superior
- .NET Framework 4.8 ou superior
- Visual Studio 2013 ou superior (para desenvolvimento)

## ⚙️ Instalação e Execução

### Opção 1: Executar aplicação compilada
1. Navegue até a pasta `ProcessamentoImagens/bin/Debug/`
2. Execute o arquivo `ProcessamentoImagens.exe`

### Opção 2: Compilar o projeto
1. Clone ou baixe o repositório
2. Abra o arquivo `ProcessamentoImagens.sln` no Visual Studio
3. Pressione `F5` ou clique em "Start" para compilar e executar

## 🎯 Como Usar

1. **Abrir Imagem**: Clique em "Abrir Imagem" e selecione um arquivo (JPG, PNG, BMP, GIF)
2. **Aplicar Filtros**: Escolha uma das operações disponíveis nos botões da interface
3. **Visualizar Resultado**: A imagem processada aparecerá no painel direito
4. **Limpar**: Use o botão "Limpar" para resetar as visualizações

### 🖼️ Interface
- **Painel Esquerdo**: Imagem original
- **Painel Direito**: Imagem processada
- **Botões Centrais**: Operações e filtros disponíveis

## 🔬 Operações Morfológicas

### Elementos Estruturantes
O projeto utiliza diferentes elementos estruturantes:

```
Elemento 3x3:        Elemento 5x5:
[-1,-1] [ 0,-1] [ 1,-1]    [-2,-2] [-1,-2] [ 0,-2] [ 1,-2] [ 2,-2]
[-1, 0] [ 0, 0] [ 1, 0]    [-2,-1] [-1,-1] [ 0,-1] [ 1,-1] [ 2,-1]
[-1, 1] [ 0, 1] [ 1, 1]    [-2, 0] [-1, 0] [ 0, 0] [ 1, 0] [ 2, 0]
                           [-2, 1] [-1, 1] [ 0, 1] [ 1, 1] [ 2, 1]
                           [-2, 2] [-1, 2] [ 0, 2] [ 1, 2] [ 2, 2]
```

### Aplicações das Operações
- **Dilatação**: Preenchimento de buracos, conexão de componentes próximos
- **Erosão**: Remoção de ruído, separação de objetos conectados
- **Abertura**: Suavização de contornos, remoção de protuberâncias
- **Fechamento**: Preenchimento de lacunas, suavização de reentrâncias

## 📁 Estrutura do Projeto

```
processamento-imagens-morfologia-e-compressao/
├── ProcessamentoImagens.sln              # Arquivo da solução
├── ProcessamentoImagens/
│   ├── Program.cs                        # Ponto de entrada da aplicação
│   ├── frmPrincipal.cs                   # Formulário principal
│   ├── frmPrincipal.Designer.cs          # Designer do formulário
│   ├── Filtros.cs                        # Implementação dos algoritmos
│   ├── ProcessamentoImagens.csproj       # Arquivo do projeto
│   └── Properties/                       # Propriedades e recursos
├── Imagens/                              # Imagens de exemplo
│   ├── fotoescura.jpg
│   ├── mathwork.png
│   ├── ImagemSegmentoRuidos.png
│   └── ...
└── README.md                             # Este arquivo
```

## 🖼️ Formatos Suportados

- **JPEG** (.jpg, .jpeg)
- **PNG** (.png)
- **BMP** (.bmp)
- **GIF** (.gif)

## 📚 Conceitos Implementados

### 🔧 Técnicas de Otimização
- **Direct Memory Access (DMA)**: Acesso direto aos dados dos pixels para maior performance
- **Unsafe Code**: Uso de ponteiros para manipulação eficiente de memória
- **BitmapData**: Travamento de bits para operações rápidas

### 🧮 Algoritmos de Processamento
- **Conversão RGB para Escala de Cinza**: Y = 0.299R + 0.587G + 0.114B
- **Binarização**: Threshold fixo em 127
- **Morfologia Matemática**: Operações com elementos estruturantes
- **Run-Length Encoding**: Compressão baseada em sequências

### 🎓 Aplicações Acadêmicas
Este projeto demonstra conceitos fundamentais de:
- Processamento Digital de Imagens
- Morfologia Matemática
- Otimização de Performance
- Programação em C# com Windows Forms
- Manipulação de Dados Binários


---