pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                sh 'dotnet build secret-friend-api/'
            }
        }
        
        stage('Test') {
            steps {
                echo 'Testing The Project..'
            }
        }
        
        stage('Deploy') {
            steps {
                echo 'Deploying The Project....'
            }
        }
    }
}
