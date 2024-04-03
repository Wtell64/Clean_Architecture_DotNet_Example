import jwtDecode from 'jwt-decode'

export default {
    setSession: function (data) {
        window.localStorage.setItem('token', JSON.stringify(data))
    },
    getSession: function () {
        return JSON.parse(window.localStorage.getItem('token'))
    },
    clearSession: function () {
        window.localStorage.removeItem('token')
    },
    hashSession: function (data) {
        let token = this.getSession()
        //return btoa(JSON.stringify(token))
        return Buffer.from(JSON.stringify(token)).toString('base64')
    },
    decodeSession: function (data) {
        let token = this.getSession()
        const encodedJwt = token.accessToken;
        const decodedJwt = jwtDecode(encodedJwt);

        const id = decodedJwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
        const email = decodedJwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
        const name = decodedJwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'];
        const role = decodedJwt['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

        return { id, email, name, role }
    },
    needsAuthentication: function () {
        let token = this.getSession()
        if (!token) {
            return true
        }
        return false
    },
    isLoggedIn: function () {
        return !this.needsAuthentication()
    }
}
