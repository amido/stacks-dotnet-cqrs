# variable "docker_image_name" {
#   type = string
# }

variable "tags" {
  description = "Meta data for labelling the infrastructure"
  type        = map(string)
  default     = {}
}

variable "env" {
  description = "The name of the environment."
  default     = "nonprod"
  type        = string
}

# variable "owner" {
#   description = "Responsible parties"
#   type        = string
# }

variable "region" {
  description = "AWS Region for this infrastruture"
  type        = string
  default     = "eu-west-2"
}

variable "enable_dynamodb" {
  description = "Whether to create dynamodb table."
  type        = bool
}

variable "enable_queue" {
  description = "Whether to create SQS queue."
  type        = bool
}

variable "queue_name" {
  description = "This is the human-readable name of the queue. If omitted, Terraform will assign a random name."
  type        = string
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

variable "name_company" {
  type = string
}

variable "name_project" {
  type = string
}
