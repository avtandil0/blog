<template>
  <v-app id="inspire">
    <v-container class="fill-height" fluid>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="8" md="4">
          <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Login form</v-toolbar-title>
              <v-spacer></v-spacer>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field
                  label="Login"
                  v-model="userName"
                  name="login"
                  prepend-icon="mdi-account"
                  type="text"
                  v-on:keyup.13="onLogin"
                ></v-text-field>

                <v-text-field
                  id="password"
                  label="Password"
                  v-model="password"
                  name="password"
                  prepend-icon="mdi-lock"
                  type="password"
                  v-on:keyup.13="onLogin"
                ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" @click="onLogin" :loading="loginLoading">Login</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<script>
export default {
  name: "Login",
  data() {
    return {
      userName: null,
      password: null,
      loginLoading: false
    };
  },
  methods: {
    async onLogin() {
      localStorage.clear();
      this.loginLoading = true;
      console.log("login1111111111", this.userName, this.password);

      var result = await this.$http.login(this.userName, this.password);
      console.log("login1111111111", result);

      if (result.id != -1) {
        console.log("###############################################", result);
        localStorage.setItem("user-token", result.token);
        localStorage.setItem(
          "currentUser",
          JSON.stringify({
            firstName: result.firstName,
            lastName: result.lastName,
            token: result.token,
            username: result.username
          })
        );

        this.$router.push("home");
      } else {
        this.loginLoading = false;
        this.$toast(result.message, {
          color: "red",
          dismissable: true
        });
        return;
      }
      // this.$router.push("home");
    }
  }
};
</script>