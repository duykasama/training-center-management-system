pipeline{
    agent any
    stages{
        stage("Build"){
            steps{
                // build gateway image
                sh "docker build -f ./FAMS.V0.ApiGateWay/Dockerfile -t ${DOCKER_REGISTRY}/fams:api-gateway ."
                // build authentication service image
                sh "docker build -f ./FAMS.V0.Services.AuthenticationService/Dockerfile -t ${DOCKER_REGISTRY}/fams:authentication-service ."
                // build user service image
                sh "docker build -f ./FAMS.V0.Services.UserService/Dockerfile -t ${DOCKER_REGISTRY}/fams:user-service ."
                // build syllabus service image
                sh "docker build -f ./FAMS.V0.Services.SyllabusService/Dockerfile -t ${DOCKER_REGISTRY}/fams:syllabus-service ."
            }
        }
        stage("Deliver"){
            steps{
                withDockerRegistry(credentialsId: 'duykasama-docker-hub', url: "https://index.docker.io/v1/") {
                    // push gateway image
                    sh "docker push ${DOCKER_REGISTRY}/fams:api-gateway"
                    // push authentication image
                    sh "docker push ${DOCKER_REGISTRY}/fams:authentication-service"
                    // push user service image
                    sh "docker push ${DOCKER_REGISTRY}/fams:user-service"
                    // push syllabus service image
                    sh "docker push ${DOCKER_REGISTRY}/fams:syllabus-service"
                    
                }
            }
        }
        stage("Deploy"){
            steps{
                sh "docker compose up -f docker-compose.prod.yml -d"
            }
        }
    }
    post{
        always{
            cleanWs()
        }
    }
}