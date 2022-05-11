# variable "docker_image_name" {
#   type = string
# }


#############
# Common Vars
#############
variable "env" {
  description = "Name of the deployment environment, like dev, staging, nonprod, prod."
  type        = string
}

variable "region" {
  description = "AWS Region for this infrastruture"
  type        = string
  default     = "eu-west-2"
}

############################################
# NAMING
############################################

variable "name_company" {
  type = string
}

variable "name_project" {
  type = string
}

variable "name_domain" {
  type = string
}

variable "name_component" {
  type = string
}

variable "stage" {
  type = string
}

variable "attributes" {
  default = []
}

variable "owner" {
  type = string
}

variable "tags" {
  type    = map(string)
  default = {}
}

# Each region must have corresponding a shortend name for resource naming purposes
variable "location_name_map" {
  type = map(string)

  default = {
    northeurope   = "eun"
    westeurope    = "euw"
    uksouth       = "uks"
    ukwest        = "ukw"
    eastus        = "use"
    eastus2       = "use2"
    westus        = "usw"
    eastasia      = "ase"
    southeastasia = "asse"
  }
}

############
# Dynamo DB
############
variable "enable_dynamodb" {
  description = "Whether to create dynamodb table."
  type        = bool
}

variable "table_name" {
  description = "The name of the table, this needs to be unique within a region."
  type        = string
}

variable "hash_key" {
  description = "The attribute to use as the hash (partition) key."
  type        = string
}

variable "attribute_name" {
  description = "Name of the attribute."
  type        = string
}

variable "attribute_type" {
  description = "Type of the attribute, which must be a scalar type: S, N, or B for (S)tring, (N)umber or (B)inary data."
}

############
# SQS
############
variable "queue_name" {
  description = "This is the human-readable name of the queue. If omitted, Terraform will assign a random name."
  type        = string
}

variable "enable_queue" {

  default     = false
  description = "Whether to create SQS queue."
  type        = bool
}
