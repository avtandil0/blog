<template>
  <v-container>
     <v-text-field label="ლინკი" v-model="videoUrl"></v-text-field>
     <v-text-field label="სათაური" v-model="title"></v-text-field>
     <v-textarea outlined rows="7" v-model="description" label="აღწერა"></v-textarea>

      <v-btn class="ma-2" outlined color="indigo" @click="save">შენახვა</v-btn>
  </v-container>
</template>

<script>
  export default {
    name: 'AddVideo',

    data: () => ({
      videoUrl: '',
      title: '',
      description: ''
    }),
    methods: {
      save(){
        console.log('aaa')
      }
    }
  }
</script>
