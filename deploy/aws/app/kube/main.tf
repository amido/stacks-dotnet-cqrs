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
