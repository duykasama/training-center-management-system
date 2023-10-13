pipeline{
    agent{
        label "node"
    }
    stages{
        stage("Build"){
            // build gateway image
            steps{
                sh "docker build -f ./FAMS.V0.ApiGateway/Dockerfile -t ${DOCKER_REGISTRY}/fams:api-gateway"
            }
            // build authentication service image
            steps{
                sh "docker build -f ./FAMS.V0.Services.AuthenticationService/Dockerfile -t ${DOCKER_REGISTRY}/fams:authentication-service"
            }
            // build user service image
            steps{
                sh "docker build -f ./FAMS.V0.Services.UserService/Dockerfile -t ${DOCKER_REGISTRY}/fams:user-service"
            }
            // build syllabus service image
            steps{
                sh "docker build -f ./FAMS.V0.Services.SyllabusService/Dockerfile -t ${DOCKER_REGISTRY}/fams:syllabus-service"
            }
        }
        stage("Deliver"){
            withDockerRegistry(credentialsId: 'duykasama-docker-hub', url: "https://index.docker.io/v1/") {
                // push gateway image
                steps{
                    sh "docker push ${DOCKER_REGISTRY}/fams:api-gateway"
                }
                // push authentication image
                steps{
                    sh "docker push ${DOCKER_REGISTRY}/fams:authentication-service"
                }
                // push user service image
                steps{
                    sh "docker push ${DOCKER_REGISTRY}/fams:user-service"
                }
                // push syllabus service image
                steps{
                    sh "docker push ${DOCKER_REGISTRY}/fams:syllabus-service"
                }
            }
        }
        stage("Deploy"){
            sh "docker compose up -f docker-compose.prod.yml -d"
        }
    }
    post{
        always{
            cleanWs()
        }
    }
}