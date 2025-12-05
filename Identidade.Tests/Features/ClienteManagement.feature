Feature: Gerenciamento de Clientes
  Como um usuário do sistema
  Eu quero poder gerenciar clientes
  Para que eu possa controlar o acesso ao sistema

  Background:
    Given que o sistema está funcionando

  Scenario: Criar um novo cliente com dados válidos
    Given que eu tenho os dados de um cliente válido
    When eu crio um novo cliente
    Then o cliente deve ser criado com sucesso
    And o cliente deve ter os dados corretos

  Scenario: Tentar criar cliente com email já existente
    Given que já existe um cliente com email "teste@teste.com"
    When eu tento criar um cliente com o mesmo email
    Then a criação deve falhar
    And deve retornar erro de email duplicado

  Scenario: Tentar criar cliente com CPF já existente
    Given que já existe um cliente com CPF "12345678900"
    When eu tento criar um cliente com o mesmo CPF
    Then a criação deve falhar
    And deve retornar erro de CPF duplicado

  Scenario: Fazer login com credenciais válidas
    Given que existe um cliente ativo com CPF "12345678900"
    When eu faço login com esse CPF
    Then o login deve ser bem-sucedido
    And deve retornar os dados do cliente

  Scenario: Fazer login com CPF inexistente
    Given que não existe cliente com CPF "99999999999"
    When eu tento fazer login com esse CPF
    Then o login deve falhar
    And deve retornar erro de cliente não encontrado

  Scenario: Fazer login com cliente inativo
    Given que existe um cliente inativo com CPF "12345678900"
    When eu tento fazer login com esse CPF
    Then o login deve falhar
    And deve retornar erro de cliente inativo

  Scenario: Alterar status de cliente existente
    Given que existe um cliente ativo com ID 1
    When eu altero o status desse cliente
    Then o status deve ser alterado com sucesso
    And o cliente deve ficar inativo

  Scenario: Tentar alterar status de cliente inexistente
    Given que não existe cliente com ID 999
    When eu tento alterar o status desse cliente
    Then a alteração deve falhar
    And deve retornar erro de cliente não encontrado
