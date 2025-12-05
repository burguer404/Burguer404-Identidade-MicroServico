variable "resource_group_name" {
  description = "Nome do Resource Group no Azure"
  type        = string
  default     = "rg-identidade"
}

variable "location" {
  description = "Localização dos recursos no Azure"
  type        = string
  default     = "brazilsouth"
}

variable "aks_cluster_name" {
  description = "Nome do cluster AKS"
  type        = string
  default     = "aks-burguer404"
}

variable "dns_prefix" {
  description = "Prefixo DNS para o cluster AKS"
  type        = string
  default     = "aksburguer404"
}

variable "node_count" {
  description = "Número de nós no node pool padrão"
  type        = number
  default     = 1
}

variable "vm_size" {
  description = "Tamanho da VM para os nós do AKS"
  type        = string
  default     = "Standard_D2as_v6"
}

variable "environment" {
  description = "Ambiente (dev, staging, prod)"
  type        = string
  default     = "dev"
}

