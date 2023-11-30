# Copyright (c) HashiCorp, Inc.
# SPDX-License-Identifier: MPL-2.0

resource "aws_instance" "main" {
  ami           = var.ami_id
  instance_type = var.instance_type

  key_name = "rmaffit"

  security_groups = [
    aws_security_group.main.name
  ]

  tags = {
    Name  = var.instance_name
    Owner = "${var.project_name}-tutorial"
  }
}

resource "aws_security_group" "main" {
  name        = "${var.instance_name}-sg"
  description = "Allow all traffic"

  ingress {
    description      = "Allow all"
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  tags = {
    Name = "allow_tls"
  }
}