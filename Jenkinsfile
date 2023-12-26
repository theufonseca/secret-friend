pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                echo 'building...'
                sh 'dotnet build secret-friend-api/'
            }
        }
        
        stage('Test') {
            steps {
                echo 'testing...'
                sh 'dotnet test secret-friend-api/'
            }
        }
        post {
            success {
                script {
                    currentBuild.result = 'SUCCESS'
                    echo 'Pipeline succeeded! Notifying GitHub...'
                    githubNotify(
                        status: 'success',
                        description: 'Pipeline succeeded',
                        context: 'Jenkins'
                    )
                }
            }
            failure {
                script {
                    currentBuild.result = 'FAILURE'
                    echo 'Pipeline failed! Notifying GitHub...'
                    githubNotify(
                        status: 'failure',
                        description: 'Pipeline failed',
                        context: 'Jenkins'
                    )
                }
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
}
