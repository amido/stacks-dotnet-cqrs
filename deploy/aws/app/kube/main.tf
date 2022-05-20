
module "app_label" {
  source  = "cloudposse/label/null"
  version = "0.25.0"

  namespace  = "${var.name_company}-${var.name_project}"
  stage      = var.stage
  name       = "${lookup(var.location_name_map, var.resource_group_location, "eu-west-2")}-${var.name_domain}"
  attributes = var.attributes
  delimiter  = "-"
  tags       = var.tags
}

module "app" {

  source = "git::https://github.com/amido/stacks-terraform//aws/modules/infrastructure_modules/stacks_app"

  enable_dynamodb = var.enable_dynamodb
  table_name      = "${module.app_label.id}-${var.table_name}"
  hash_key        = var.hash_key
  attribute_name  = var.attribute_name
  attribute_type  = var.attribute_type
  enable_queue    = var.enable_queue
  queue_name      = "${module.app_label.id}-${var.queue_name}"
  tags            = module.app_label.tags
}

resource "aws_ecr_repository" "docker_image" {
  name = var.docker_image_name
  image_scanning_configuration {
    scan_on_push = false
  }
  # Pass Default Tag Values to Underlying Modules
  tags = var.tags
}

resource "aws_ecr_repository" "docker_image_bg_worker" {
  name = var.docker_image_name_bg_worker
  image_scanning_configuration {
    scan_on_push = false
  }
  # Pass Default Tag Values to Underlying Modules
  tags = var.tags
}

resource "aws_ecr_repository" "docker_image_worker_function" {
  name = var.docker_image_name_worker
  image_scanning_configuration {
    scan_on_push = false
  }
  # Pass Default Tag Values to Underlying Modules
  tags = var.tags
}

resource "aws_ecr_repository" "docker_image_asb_function" {
  name = var.docker_image_name_asb_listener
  image_scanning_configuration {
    scan_on_push = false
  }
  # Pass Default Tag Values to Underlying Modules
  tags = var.tags
}

resource "aws_ecr_repository" "docker_image_aeh_function" {
  name = var.docker_image_name_aeh_listener
  image_scanning_configuration {
    scan_on_push = false
  }
  # Pass Default Tag Values to Underlying Modules
  tags = var.tags
}
