@(
    @{
        displayName = "AppDeployment"
        template = "deploy/k8s/app/base_api-deploy.yml"
        vars = @{
            dns_pointer = "`${ENV_NAME}-`${DOMAIN}.`${DNS_BASE_DOMAIN}"
            environment = "`${ENV_NAME}"
            tls_domain = "`${DNS_BASE_DOMAIN}"
            k8s_app_route = "`${K8S_APP_ROUTE}"
            log_level = "Debug"
            k8s_image = "`${DOCKER_REGISTRY}/`${DOCKER_IMAGE_NAME}:`${DOCKER_IMAGE_TAG}"
            aadpodidentitybinding = "stacks-webapp-identity"
            app_insights_key = "`${APP_INSIGHTS_INSTRUMENTATION_KEY}"
            cosmosdb_key = "`${COSMOSDB_PRIMARY_MASTER_KEY}" # TODO: Set this using Invoke Templater!
            cosmosdb_endpoint = "`${COSMOSDB_ENDPOINT}" # TODO: Set this using Invoke Templater!
            cosmosdb_name = "`${COSMOSDB_DATABASE_NAME}" # TODO: Set this using Invoke Templater!
            version = "`${DOCKER_IMAGE_TAG}"
            jwtbearerauthentication_audience = "<TODO>"
            jwtbearerauthentication_authority = "<TODO>"
            jwtbearerauthentication_enabled = false # Temporarily disabled directly in template file
            jwtbearerauthentication_openapiauthorizationurl = "<TODO>"
            jwtbearerauthentication_openapiclientid = "<TODO>"
            jwtbearerauthentication_openapitokenurl = "<TODO>"
            rewrite_target = '/$([char]0x0024)2' # Using UniCode to prevent substitution
        }
    }
)
