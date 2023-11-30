output "ip_address" {
  value = module.ec2-instance.ip_address
}

output "instance_name" {
  value = random_pet.instance.id
}