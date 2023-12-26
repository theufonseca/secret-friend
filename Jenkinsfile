pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                echo 'building....'
                sh 'dotnet build secret-friend-api/'
            }
        }
        
        stage('Test') {
            steps {
                echo 'testing....'
                sh 'dotnet test secret-friend-api/'
            }
        }
        // stage('Deploy') {
        //     steps {
        //         echo 'deploying...'
        //         sh 'dotnet publish -c Release -o ./publish secret-friend-api/'
        //         sh 'cd publish && dotnet secret-friend-api.dll'
        //     }
        // }
        // Teste Jenkins/git webhook
    }
    post {
        success {
            script {
                currentBuild.result = 'SUCCESS'
                echo 'Pipeline succeeded! Notifying GitHub.......'
                githubNotify(
                    status: 'SUCCESS',
                    description: 'Pipeline succeeded',
                    context: 'Jenkins',
                    repo: 'theufonseca/secret-friend', // Substitua com o caminho do seu repositório no GitHub
                    credentialsId: 'edb6dbc4-ed8c-44cb-b24c-e0745d3ddc2f', // Substitua com o ID de suas credenciais no Jenkins
                    account: 'joaomatheus_fonseca@hotmail.com', // Substitua com o seu nome de usuário no GitHub
                    sha: env.GIT_COMMIT // Use a variável de ambiente GIT_COMMIT para obter o SHA da confirmação atual
                )
            }
        }
        failure {
            script {
                currentBuild.result = 'FAILURE'
                echo 'Pipeline failed! Notifying GitHub.......'
                githubNotify(
                    status: 'FAILURE',
                    description: 'Pipeline failed',
                    context: 'Jenkins',
                    repo: 'theufonseca/secret-friend', // Substitua com o caminho do seu repositório no GitHub
                    credentialsId: 'edb6dbc4-ed8c-44cb-b24c-e0745d3ddc2f', // Substitua com o ID de suas credenciais no Jenkins
                    account: 'joaomatheus_fonseca@hotmail.com', // Substitua com o seu nome de usuário no GitHub
                    sha: env.GIT_COMMIT // Use a variável de ambiente GIT_COMMIT para obter o SHA da confirmação atual
                )
            }
        }
    }
}
