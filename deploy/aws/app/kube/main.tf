# resource "aws_ecr_repository" "docker_image" {
#   name = var.docker_image_name
#   image_scanning_configuration {
#     scan_on_push = false
#   }
#   # Pass Default Tag Values to Underlying Modules
#   tags = module.default_label.tags
# }

module "default_label" {
  source     = "git::https://github.com/cloudposse/terraform-null-label.git?ref=0.24.1"
  namespace  = "${var.name_company}-${var.name_project}"
  stage      = var.stage
  name       = "${var.region}-${var.name_domain}"
  attributes = var.attributes
  delimiter  = "-"
  tags       = var.tags
}

resource "random_string" "random" {
  length  = 3
  special = false
}

module "server_side_app" {

  source = "git::https://github.com/amido/stacks-terraform//aws/modules/infrastructure_modules/stacks_app"

  enable_dynamodb = var.enable_dynamodb ? 1 : 0
  table_name      = "${var.table_name}-${random_string.random.result}"
  hash_key        = var.hash_key
  attribute_name  = var.attribute_name
  attribute_type  = var.attribute_type
  enable_queue    = var.enable_queue
  queue_name      = var.queue_name
  env             = var.env
  tags            = module.default_label.tags
}
