variable "docker_image_name" {
  description = "Main docker image"
  type = string
}

variable "docker_image_name_bg_worker" {
  description = "BG Worker docker image name"
  type = string
}

variable "docker_image_name_worker" {
  description = "K8S Worker docker image name"
  type = string
}

variable "docker_image_name_asb_listener" {
  description = "ASB Listener docker image name"
  type = string
}

variable "docker_image_name_aeh_listener" {
  description = "ASB Listener docker image name"
  type = string
}
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

variable "owner" {
  description = "Responsible parties"
  type        = string
}

variable "region" {
  description = "AWS Region for this infrastruture"
  type        = string
  default     = "eu-west-2"
}
