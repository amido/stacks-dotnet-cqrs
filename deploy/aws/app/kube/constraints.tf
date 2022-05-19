terraform {
  required_version = ">= 1.1"

  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = ">= 3.37.0"
    }

    terraform-null-label = {
      source  = "cloudposse/label/null"
      version = "0.25.0"
    }

  }
}
