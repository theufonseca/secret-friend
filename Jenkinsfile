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
    }
    
    post {
        success {
            script {
                currentBuild.result = 'SUCCESS'
                echo 'Pipeline succeeded! Notifying GitHub.......'
                notifyGitHub('success')
            }
        }
        failure {
            script {
                currentBuild.result = 'FAILURE'
                echo 'Pipeline failed! Notifying GitHub.......'
                notifyGitHub('failure')
            }
        }
    }
}

def notifyGitHub(status) {
    def commitSHA = sh(script: 'git rev-parse HEAD', returnStdout: true).trim()

    def apiUrl = "https://api.github.com/repos/theufonseca/secret-friend/statuses/${commitSHA}"
    def credentialsId = 'GitHubAccessToken'

    withCredentials([string(credentialsId: credentialsId, variable: 'GITHUB_TOKEN')]) {
        def payload = """{
            "state": "${status.toLowerCase()}",
            "description": "Pipeline ${status.capitalize()}",
            "context": "Jenkins"
        }"""

        sh "curl -X POST ${apiUrl} -H \"Authorization: token ${GITHUB_TOKEN}\" -d '${payload}'"
    }
}
